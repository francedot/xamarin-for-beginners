using System;
using Movies.ViewModels;
using Xamarin.Forms;

namespace Movies.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPageViewModel ViewModel => this.BindingContext as LoginPageViewModel;

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var el = sender as Element;
            if (el == null)
            {
                return;
            }

            if (await ViewModel.TryLoginAsync(el.ClassId))
            {
                App.Instance.MainPage = new MainPage();
            }
            else
            {
                await DisplayAlert("Authentication Problem",
                    "There was an issue while authenticating", "Cancel");
            }
        }
    }
}