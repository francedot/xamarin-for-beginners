using System;
using Movies.ViewModels;
using Xamarin.Forms;

namespace Movies.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPageViewModel ViewModel => this.BindingContext as MenuPageViewModel;

        public MenuPage()
        {
            InitializeComponent();
        }
    }
}