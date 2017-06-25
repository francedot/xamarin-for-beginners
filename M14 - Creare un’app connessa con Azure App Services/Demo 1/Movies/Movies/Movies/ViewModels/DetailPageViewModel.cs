using System.Collections.Generic;
using Movies.Models;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        private IList<Movie> _movies;

        public DetailPageViewModel()
        {
            RefreshCommand = new Command(() =>
            {
                MessagingCenter.Send<DetailPageViewModel>(this, "RefreshMovies");
            });
            DeleteCommand = new Command<Movie>(async movie =>
            {
                await MoviesSource.RemoveMovieAsync(movie.Id);
                MessagingCenter.Send<DetailPageViewModel>(this, "RefreshMovies");
            });
            NavigateCommand = new Command<Movie>(movie =>
            {
                MessagingCenter.Send<DetailPageViewModel, Movie>(this, "SelectedMovie", movie);
            });
        }

        public IList<Movie> Movies
        {
            get => _movies;
            set => Set(ref _movies, value);
        }

        public Command RefreshCommand { get; }
        public Command<Movie> DeleteCommand { get; }
        public Command<Movie> NavigateCommand { get; }
    }
}
