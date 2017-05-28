using System;
using System.Collections.Generic;
using Movies.ViewModels;
using Xamarin.Forms;

namespace Movies.Views
{
    public partial class DetailPage : CarouselPage
    {
        public DetailPageViewModel ViewModel => this.BindingContext as DetailPageViewModel;

        public DetailPage()
        {
            InitializeComponent();
        }
    }
}