using Movies.ViewModels;
using Xamarin.Forms;

namespace Movies.Views
{
    public partial class DetailPage : ContentPage
    {
        public DetailPageViewModel ViewModel => this.BindingContext as DetailPageViewModel;

        public DetailPage()
        {
            InitializeComponent();
        }


        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (ViewModel?.NavigateCommand != null && ViewModel.NavigateCommand.CanExecute(e.Item))
            {
                this.Navigation.PushAsync(new MoviePage());
                ViewModel.NavigateCommand.Execute(e.Item);
            }
        }
    }
}