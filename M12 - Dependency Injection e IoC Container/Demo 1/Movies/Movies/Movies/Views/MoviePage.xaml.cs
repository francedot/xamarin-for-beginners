using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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