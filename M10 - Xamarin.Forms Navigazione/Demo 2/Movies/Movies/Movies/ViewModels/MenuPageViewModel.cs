using System.Collections.Generic;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        private IList<string> _genres;
        private string _selectedGenre;

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
    }
}
