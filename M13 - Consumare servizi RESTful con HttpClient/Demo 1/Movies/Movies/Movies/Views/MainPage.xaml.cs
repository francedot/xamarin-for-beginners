using System.Linq;
using Movies.Services;
using Movies.ViewModels;
using Xamarin.Forms;

namespace Movies.Views
{
    public partial class MainPage : MasterDetailPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = _viewModel = new MainPageViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAppearingAsync();

            //var source = new MoviesWebSource();

            //var movies = await source.GetMoviesAsync();
            //await source.DeleteMovieAsync(0);


            ////await source.PutMovieAsync(0, movie1);

            ////movies.Insert(0, movie);

            ////await source.PostMoviesAsync(movies.ToList());

            //var updatedMovies = await source.GetMoviesAsync();
        }
    }
}
