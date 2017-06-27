using Movies.Services;
using Movies.Views;
using Xamarin.Forms;

namespace Movies
{
    public partial class App : Application
    {
        public MobileAppAuthenticator Authenticator => MobileAppAuthenticator.Instance;

        public App()
        {
            Instance = this;
            InitializeComponent();

            var targetPage = Authenticator.IsUserLogged() ? 
                            (Page) new MainPage() : 
                                   new LoginPage();

            MainPage = targetPage;
        }

        public static App Instance { get; private set; }

        public void ChangeRoot(bool login)
        {
            MainPage = login ? (Page) new LoginPage() : new MainPage();
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
