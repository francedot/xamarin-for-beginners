using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Services
{
    public class MoviesWebSource : IMoviesSource
    {
        private string BaseUriString { get; } = "http://moviesbackend.azurewebsites.net/api/";
        
        #region CRUD

        public async Task<IList<Movie>> GetMoviesAsync()
        {
            var movies =
                await JsonRestApi.GetAsync<List<Movie>>($"{BaseUriString}movies");

            return movies;
        }

        public async Task<Movie> GetMovieAsync(int id)
        {
            var movie =
                await JsonRestApi.GetAsync<Movie>($"{BaseUriString}movies/{id}");

            return movie;
        }

        public async Task PostMoviesAsync(List<Movie> movies)
        {
            await JsonRestApi.PostAsync<List<Movie>>($"{BaseUriString}movies", movies);
        }

        public async Task PutMovieAsync(int id, Movie movie)
        {
            await JsonRestApi.PutAsync<Movie>($"{BaseUriString}movies/{id}", movie);
        }


        public async Task DeleteMovieAsync(int id)
        {
            await JsonRestApi.DeleteAsync($"{BaseUriString}movies/{id}");
        }

        #endregion
    }
}