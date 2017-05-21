using System.Collections.Generic;
using Movies.Models;

namespace Movies.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        private IList<Movie> _movies;

        public IList<Movie> Movies
        {
            get => _movies;
            set => Set(ref _movies, value);
        }
    }
}
