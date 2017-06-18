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
        }
    }
}
