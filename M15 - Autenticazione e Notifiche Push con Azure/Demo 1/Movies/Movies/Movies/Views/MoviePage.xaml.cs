using Movies.ViewModels;
using Xamarin.Forms;

namespace Movies.Views
{
    public partial class MoviePage : TabbedPage
    {
        private  MoviePageViewModel ViewModel => this.BindingContext as MoviePageViewModel;

        public MoviePage()
        {
            InitializeComponent();
        }
    }
}