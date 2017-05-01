using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Profile
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            this.BindingContext = _viewModel = new MainPageViewModel();

            InitializeComponent();
        }
    }
}
