using System;
using System.Windows.Input;
using Profile.Services;
using Xamarin.Forms;

namespace Profile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ProfileViewModel _profile;

        public MainPageViewModel()
        {
            _profile = new ProfileViewModel(MockProfileProvider.GetProfile());
            NavigateCommand = new Command(() =>
            {
                Device.OpenUri(new Uri("https://twitter.com/francedot"));
            });
        }

        public ICommand NavigateCommand { get; }

        public ProfileViewModel Profile
        {
            get { return _profile; }
            set { Set(ref _profile, value); }
        }
    }
}