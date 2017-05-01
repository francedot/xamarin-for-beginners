using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Profile.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Profile _profile;

        public ProfileViewModel(Profile profile)
        {
            _profile = profile;
        }

        public string Name
        {
            get => _profile.Name;
            set
            {
                if (_profile.Name == value)
                    return;
                _profile.Name = value;
                OnPropertyChanged();
            }
        }

        public string BackgroundImage
        {
            get => _profile.BackgroundImage;
            set
            {
                if (_profile.BackgroundImage == value)
                    return;
                _profile.BackgroundImage = value;
                OnPropertyChanged();
            }
        }

        public string ProfileImage
        {
            get => _profile.ProfileImage;
            set
            {
                if (_profile.ProfileImage == value)
                    return;
                _profile.ProfileImage = value;
                OnPropertyChanged();
            }
        }

        public string Tag
        {
            get => _profile.Tag;
            set
            {
                if (_profile.Tag == value)
                    return;
                _profile.Tag = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _profile.Description;
            set
            {
                if (_profile.Description == value)
                    return;
                _profile.Description = value;
                OnPropertyChanged();
            }
        }

        public uint Likes
        {
            get => _profile.Likes;
            set
            {
                if (_profile.Likes == value)
                    return;
                _profile.Likes = value;
                OnPropertyChanged();
            }
        }

        public uint Following
        {
            get => _profile.Following;
            set
            {
                if (_profile.Following == value)
                    return;
                _profile.Following = value;
                OnPropertyChanged();
            }
        }

        public uint Followers
        {
            get => _profile.Followers;
            set
            {
                if (_profile.Followers == value)
                    return;
                _profile.Followers = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}