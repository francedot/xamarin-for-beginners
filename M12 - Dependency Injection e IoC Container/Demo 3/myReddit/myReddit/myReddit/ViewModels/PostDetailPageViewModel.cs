using System.Collections.Generic;
using System.Linq;
using MyReddit.Models;
using MyReddit.Services;
using Prism.Mvvm;
using Prism.Navigation;

namespace MyReddit.ViewModels
{
    public class PostDetailPageViewModel : BindableBase, INavigationAware
    {
        private bool _areCommentsLoading = true;
        private Post _post;
        private IList<Comment> _comments;
        
        private readonly INavigationService _navigationService;
        private readonly IRedditApiSource _redditApiSource;

        public PostDetailPageViewModel(INavigationService navigationService, IRedditApiSource redditApiSource)
        {
            _navigationService = navigationService;
            _redditApiSource = redditApiSource;
        }

        public bool AreCommentsLoading
        {
            get { return _areCommentsLoading; }
            set { SetProperty(ref _areCommentsLoading, value); }
        }

        public Post Post
        {
            get { return _post; }
            set { SetProperty(ref _post, value); }
        }

        public IList<Comment> Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            // NOP
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!parameters.ContainsKey("currentPost"))
            {
                // First Time TODO Manage
                return;
            }
            Post = parameters["currentPost"] as Post;
            AreCommentsLoading = true;
            Comments = (await _redditApiSource.GetCommentsAsync(Post)).Take(10).ToList();
            AreCommentsLoading = false;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
