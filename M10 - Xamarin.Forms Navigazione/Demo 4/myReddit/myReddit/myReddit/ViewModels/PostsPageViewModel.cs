using System.Collections.Generic;
using MyReddit.Models;
using MyReddit.Navigation;
using MyReddit.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MyReddit.ViewModels
{
    public class PostsPageViewModel : BindableBase, INavigationAware
    {
        private bool _isLoading = true;
        private IList<Post> _posts;
        private Subreddit _currentSubreddit;

        private readonly INavigationService _navigationService;
        private readonly IRedditApiSource _redditApiSource;

        public PostsPageViewModel(INavigationService navigationService, IRedditApiSource redditApiSource)
        {
            _navigationService = navigationService;
            _redditApiSource = redditApiSource;

            ShowPostDetailCommand = new DelegateCommand<Post>(async (post) =>
            {
                var navParams = new NavigationParameters
                {
                    {"currentPost", post}
                };

                await navigationService.NavigateAsync(PageTokens.PostDetailPage, navParams);
            });
        }

        public DelegateCommand<Post> ShowPostDetailCommand { get; private set; }

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public IList<Post> Posts
        {
            get { return _posts; }
            set { SetProperty(ref _posts, value); }
        }

        public Subreddit CurrentSubreddit
        {
            get { return _currentSubreddit; }
            set { SetProperty(ref _currentSubreddit, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            // NOP
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!parameters.ContainsKey("currentSubreddit"))
            {
                // First Time TODO Manage
                return;
            }
            CurrentSubreddit = parameters["currentSubreddit"] as Subreddit;
            IsLoading = true;
            Posts = await _redditApiSource.GetPostsAsync(CurrentSubreddit);
            IsLoading = false;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }
    }
}
