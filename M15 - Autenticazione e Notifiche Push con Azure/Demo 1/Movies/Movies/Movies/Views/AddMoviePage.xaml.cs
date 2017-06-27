using System;
using Movies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMoviePage : ContentPage
    {
        public AddMoviePageViewModel ViewModel => this.BindingContext as AddMoviePageViewModel;

        public AddMoviePage()
        {
            InitializeComponent();
        }

        private void SaveMovie_OnClicked(object sender, EventArgs e)
        {
            if (ViewModel?.SaveMovieCommand != null && ViewModel.SaveMovieCommand.CanExecute(null))
            {
                this.Navigation.PopAsync(true);
                ViewModel.SaveMovieCommand.Execute(null);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.OnAppearingAsync();
        }
    }
}