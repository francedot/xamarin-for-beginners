using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Movies.Models;

namespace Movies.Services
{
    public class MobileAppMoviesSource : IMoviesSource
    {
        #region Mobile App

        private MobileServiceClient _mobileServiceClient;
        private IMobileServiceSyncTable<Movie> _moviesTable;
        private static readonly string MobileAppUrl = "http://moviesmobileapp.azurewebsites.net";
        private const string DbName = "syncmovies.db";
        private static readonly string SqLiteDbPath = Path.Combine(MobileServiceClient.DefaultDatabasePath, DbName);

        public async Task Initialize()
        {
            _mobileServiceClient = new MobileServiceClient(MobileAppUrl);

            var store = new MobileServiceSQLiteStore(SqLiteDbPath);

            store.DefineTable<Movie>();

            await _mobileServiceClient.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            _moviesTable = _mobileServiceClient.GetSyncTable<Movie>();
        }

        private async Task InitAndSync()
        {
            if (!(_mobileServiceClient?.SyncContext?.IsInitialized ?? false))
            {
                await Initialize();
            }
            await SyncMovies();
        }

        public async Task SyncMovies()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            try
            {
                await _mobileServiceClient.SyncContext.PushAsync();
            }
            catch (MobileServicePushFailedException e)
            {
                if (e.PushResult != null)
                {
                    syncErrors = e.PushResult.Errors;
                }
            }

            // Error/conflict handling.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        // Revert to server's copy
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine($"Error executing sync operation. Item: {error.TableName} ({error.Item["id"]}). Operation discarded.");
                }
            }

            try
            {
                await _moviesTable.PullAsync("allMovies", _moviesTable.CreateQuery());
            }
            catch (Exception )
            {
                // It is ok if we are not connected
            }
        }

        #endregion

        #region IMoviesSource Implementation

        public async Task<IList<Movie>> GetMoviesAsync()
        {
            await InitAndSync();
            return await _moviesTable.OrderBy(m => m.Genre).
                                      ToListAsync();
        }

        public async Task<Movie> GetMovieAsync(string id)
        {
            await InitAndSync();
            return await _moviesTable.LookupAsync(id);
        }

        public async Task UpdateMoviesAsync(List<Movie> movies)
        {
            await InitAndSync();
            await _moviesTable.PurgeAsync();
            foreach (var movie in movies)
            {
                await _moviesTable.InsertAsync(movie);
            }
        }

        public async Task InsertMovieAsync(Movie movie, string id = null)
        {
            await InitAndSync();
            await _moviesTable.InsertAsync(movie);
        }

        public async Task RemoveMovieAsync(string id)
        {
            await InitAndSync();
            var toDelete = await _moviesTable.LookupAsync(id);
            await _moviesTable.DeleteAsync(toDelete);
        }

        #endregion
    }
}