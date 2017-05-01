using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Profile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var model = new Profile
            {
                Name = "Francesco",
                BackgroundImage = "Assets/BackgroundImage.jpg",
                ProfileImage = "https://avatars0.githubusercontent.com/u/11706033?v=3&s=400",
                Description = "Jogger and TV series addicted. In spare time enjoy turning coffee into XAML while listening to good music.",
                Tag = "Xamarin Developer",
                Likes = 47,
                Following = 900,
                Followers = 957
            };
            Resources["myProfile"] = model;

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
