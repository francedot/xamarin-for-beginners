using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Services;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        private IList<string> _genres;
        private string _selectedGenre;

        public MenuPageViewModel()
        {
            LogoutCommand = new Command(async () =>
            {
                await MobileAppAuthenticator.Instance.LogoutAsync();
                Device.BeginInvokeOnMainThread(
                    () => App.Instance.ChangeRoot(login: true));
            });
        }

        public Command LogoutCommand { get;}

        public IList<string> Genres
        {
            get => _genres;
            set => Set(ref _genres, value);
        }
        public string SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                Set(ref _selectedGenre, value);
                MessagingCenter.Send<MenuPageViewModel, string>(this, "SelectedGenre", value);
            }
        }

        public void UpdateGenres()
        {
            var splittedGenres = new List<string>();
            var genres = AllMovies.Select(m => m.Genre);
            foreach (var genre in genres)
            {
                splittedGenres.AddRange(genre.Split(',').Select(s => s.Trim()));
            }
            Genres = splittedGenres.Distinct().ToList();
            SelectedGenre = Genres.FirstOrDefault();
        }
    }
}
