namespace Profile.Services
{
    public static class MockProfileProvider
    {
        public static Profile GetProfile()
        {
            return new Profile
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
        }
    }
}