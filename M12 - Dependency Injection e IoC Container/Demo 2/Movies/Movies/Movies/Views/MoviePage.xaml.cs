using Movies.ViewModels;
using Xamarin.Forms;

namespace Movies.Views
{
    public partial class MoviePage : TabbedPage
    {
        private readonly MoviePageViewModel _viewModel;

        public MoviePage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new MoviePageViewModel();
        }
    }
}