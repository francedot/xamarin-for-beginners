using Microsoft.Practices.Unity;
using MyReddit.Navigation;
using MyReddit.Services;
using MyReddit.Views;
using Prism.Unity;
using Xamarin.Forms.Xaml;

namespace MyReddit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await
                NavigationService.NavigateAsync(
                    $"{PageTokens.RootMasterDetailPage}/{PageTokens.RootNavigationPage}/{PageTokens.PostsPage}",
                    animated: false);
            //NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<IRedditApiSource, RedditApiSource>();

            Container.RegisterTypeForNavigation<RootMasterDetailPage>();
            Container.RegisterTypeForNavigation<RootMasterDetailPage>();
            Container.RegisterTypeForNavigation<RootNavigationPage>();
            Container.RegisterTypeForNavigation<SubredditsMenuPage>();
            Container.RegisterTypeForNavigation<PostsPage>();
            Container.RegisterTypeForNavigation<PostDetailPage>();
        }
    }
}