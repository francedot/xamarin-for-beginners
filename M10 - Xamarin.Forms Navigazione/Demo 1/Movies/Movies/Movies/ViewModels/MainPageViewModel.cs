using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Models;
using Movies.Services;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly MoviesLoader _moviesLoader = new MoviesLoader();
        
        public MainPageViewModel()
        {
            MessagingCenter.Subscribe<MenuPageViewModel, string>(this, "SelectedGenre", (sender, selectedGenre) =>
            {
                DetailPageViewModel.Movies = AllMovies.Where(m => m.Genre.Contains(MenuPageViewModel.SelectedGenre)).ToList();
            });

            MenuPageViewModel = new MenuPageViewModel();
            DetailPageViewModel = new DetailPageViewModel();
        }
        
        public MenuPageViewModel MenuPageViewModel { get; set; }
        public DetailPageViewModel DetailPageViewModel { get; set; }

        public IList<Movie> AllMovies { get; set; }

        public async Task OnAppearingAsync()
        {
            AllMovies = await _moviesLoader.LoadMoviesAsync();

            var splittedGenres = new List<string>();
            var genres = AllMovies.Select(m => m.Genre);
            foreach (var genre in genres)
            {
                splittedGenres.AddRange(genre.Split(',').Select(s => s.Trim()));
            }
            MenuPageViewModel.Genres = splittedGenres.Distinct().ToList();

            MenuPageViewModel.SelectedGenre = MenuPageViewModel.Genres.FirstOrDefault();
        }
    }
}
