using System;
using System.Collections.Generic;
using System.Windows.Input;
using Movies.Models;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        private IList<Movie> _movies;

        public DetailPageViewModel()
        {
        }

        public IList<Movie> Movies
        {
            get => _movies;
            set => Set(ref _movies, value);
        }
    }
}
