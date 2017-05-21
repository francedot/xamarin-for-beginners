using System.Collections.Generic;
using Movies.Models;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class MoviePageViewModel : ViewModelBase
    {
        private Movie _movie;

        public MoviePageViewModel()
        {
            MessagingCenter.Subscribe<DetailPageViewModel, Movie>(this, "SelectedMovie", (model, movie) =>
            {
                Movie = movie;
            });
        }

        public Movie Movie
        {
            get => _movie;
            set => Set(ref _movie, value);
        }
    }
}
