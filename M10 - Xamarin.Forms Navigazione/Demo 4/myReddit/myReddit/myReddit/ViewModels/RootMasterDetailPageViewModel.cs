using System.Collections.Generic;
using System.Linq;
using MyReddit.Helpers;
using MyReddit.Models;
using MyReddit.Navigation;
using MyReddit.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MyReddit.ViewModels
{
    public class RootMasterDetailPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IRedditApiSource _redditApiSource;
        private Subreddit _currentSubreddit;

        private IList<Grouping<string, Subreddit>> _menuGroupings;

        public RootMasterDetailPageViewModel(INavigationService navigationService, IRedditApiSource redditApiSource)
        {
            _navigationService = navigationService;
            _redditApiSource = redditApiSource;

            ChangeSubredditCommand = new DelegateCommand<Subreddit>(async subReddit =>
            {
                var navParams = new NavigationParameters
                {
                    {"currentSubreddit", subReddit}
                };
                CurrentSubreddit = subReddit;
                await navigationService.NavigateAsync($"{PageTokens.RootNavigationPage}/{PageTokens.PostsPage}",
                    navParams);
                // or
                //await _navigationService.NavigateAsync($"{PageTokens.RootNavigationPage}/{PageTokens.PostsPage}?subredditTitle={subReddit.Title}");            
            });
        }

        public DelegateCommand<Subreddit> ChangeSubredditCommand { get; }

        public Subreddit CurrentSubreddit
        {
            get => _currentSubreddit;
            set => SetProperty(ref _currentSubreddit, value);
        }

        public IList<Grouping<string, Subreddit>> MenuGroupings
        {
            get => _menuGroupings;
            set => SetProperty(ref _menuGroupings, value);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            // NOP
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (MenuGroupings != null)
                return;
            var subReddits = await _redditApiSource.GetSubredditsAsync();
            MenuGroupings = GetMenuGroupings(subReddits);
            CurrentSubreddit = MenuGroupings.FirstOrDefault()?.Values.FirstOrDefault();
            ChangeSubredditCommand.Execute(_currentSubreddit);
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private static IList<Grouping<string, Subreddit>> GetMenuGroupings(IList<Subreddit> subReddits)
        {
            return new List<Grouping<string, Subreddit>>
            {
                new Grouping<string, Subreddit>("SUBREDDITS", subReddits)
            };
        }
    }
}