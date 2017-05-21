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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
            {
                ViewModel.GenreChanged += OnGenreChanged;
            }
        }

        private void OnGenreChanged(object sender, EventArgs eventArgs)
        {
            this.Children.Clear();

            foreach (var movie in ViewModel.Movies)
            {
                this.Children.Add(new MoviePage()
                {
                    BindingContext = movie
                });
            }
        }
    }
}