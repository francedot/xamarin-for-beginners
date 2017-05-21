using System.Collections.Generic;
using System.Threading.Tasks;
using MyReddit.Extensions;
using MyReddit.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace MyReddit.Services
{
    public class RedditApiSource : IRedditApiSource
    {
        private readonly JsonSerializerSettings _serializerSettings;

        private string BaseUriString { get; } = "https://www.reddit.com/";

        private RestApi RestApi => RestApi.Instance;

        public RedditApiSource()
        {
            _serializerSettings = new JsonSerializerSettings()
            {
                Error = OnError
            };
        }

        #region GET

        public async Task<IList<Subreddit>> GetSubredditsAsync()
        {
            IList<Subreddit> subReddits = null;

            var content =
                await RestApi.GetRequestAsync(BaseUriString + "reddits.json");

            if (content == null)
                return null;

            subReddits = DeserializeSubredditsJson(content);

            return subReddits;
        }

        private IList<Subreddit> DeserializeSubredditsJson(string content)
        {
            var result = new List<Subreddit>();

            var responseJObject = JObject.Parse(content);
            var dataProperty = responseJObject["data"];
            var childrenProperty = dataProperty["children"];
            var subreddits = JArray.Parse(childrenProperty.ToString());

            foreach (var subreddit in subreddits)
            {
                result.Add(DeserializeSubredditJson(subreddit.ToString()));
            }

            return result;
        }

        private Subreddit DeserializeSubredditJson(string subRedditContent)
        {
            var subredditJObject = JObject.Parse(subRedditContent);
            var dataProperty = subredditJObject["data"];
            var displayName = dataProperty["display_name"].ToString();
            var iconUrl = dataProperty["icon_img"].ToString();

            return new Subreddit()
            {
                Title = displayName,
                IconUrl = iconUrl
            };
        }

        public async Task<IList<Post>> GetPostsAsync(Subreddit subreddit)
        {
            IList<Post> threads = null;

            var content =
                await RestApi.GetRequestAsync(BaseUriString + $"r/{subreddit.Title}/top/.json?limit=30&sort=new");

            if (content == null)
                return null;

            threads = DeserializePostsJson(content);

            return threads;
        }

        private IList<Post> DeserializePostsJson(string content)
        {
            var result = new List<Post>();

            var responseJObject = JObject.Parse(content);
            var dataProperty = responseJObject["data"];
            var childrenProperty = dataProperty["children"];
            var posts = JArray.Parse(childrenProperty.ToString());

            foreach (var post in posts)
            {
                result.Add(DeserializePostJson(post.ToString()));
            }

            return result;
        }

        private Post DeserializePostJson(string postContent)
        {
            var postJObject = JObject.Parse(postContent);
            var dataProperty = postJObject["data"];
            var id = dataProperty["id"].ToString();
            var content = dataProperty["selftext"].ToString();
            var author = dataProperty["author"].ToString();
            var title = dataProperty["title"].ToString();
            var subreddit = dataProperty["subreddit"].ToString();
            var createdUtc = dataProperty["created_utc"].ToString();
            var createdDateLong = long.Parse(createdUtc);
            var createdDate = createdDateLong.ToDateTimeFromEpoch();
            var likesRaw = dataProperty["ups"].ToString();
            var likes = string.IsNullOrEmpty(likesRaw) ? 0 : int.Parse(likesRaw);
            var numCommentsRaw = dataProperty["num_comments"].ToString();
            var numComments = string.IsNullOrEmpty(numCommentsRaw) ? 0 : int.Parse(numCommentsRaw);
            var thumbnailUri = dataProperty["thumbnail"].ToString();
            if (string.IsNullOrEmpty(thumbnailUri) || thumbnailUri.Trim() == "self")
            {
                thumbnailUri = "Assets/Placeholder.jpg";
            }

            return new Post()
            {
                Id = id,
                Title = title,
                Subreddit = subreddit,
                Content = content,
                Author = author,
                DateCreated = createdDate,
                Likes = likes,
                CommentsCount = numComments,
                ThumbnailUri = thumbnailUri
            };
        }

        public async Task<IList<Comment>> GetCommentsAsync(Post post)
        {
            IList<Comment> comments = null;

            //ttp://www.reddit.com/r/" + sub + "/comments/" + id + ".json

            var content =
                await RestApi.GetRequestAsync(BaseUriString + $"r/{post.Subreddit}/comments/{post.Id}.json?limit=30");

            if (content == null)
                return null;

            comments = DeserializeCommentsJson(content);

            return comments;
        }

        private IList<Comment> DeserializeCommentsJson(string content)
        {
            var result = new List<Comment>();

            var responseJArray = JArray.Parse(content);
            var responseJObject = JObject.Parse(responseJArray[1].ToString());
            var dataProperty = responseJObject["data"];
            var childrenProperty = dataProperty["children"];
            var comments = JArray.Parse(childrenProperty.ToString());

            foreach (var comment in comments)
            {
                result.Add(DeserializeCommentJson(comment.ToString()));
            }

            return result;
        }

        private Comment DeserializeCommentJson(string commentContent)
        {
            var postJObject = JObject.Parse(commentContent);
            var dataProperty = postJObject["data"];
            var author = dataProperty["author"]?.ToString();
            var content = dataProperty["body"]?.ToString();
            var createdUtc = dataProperty["created_utc"]?.ToString();
            var createdDateLong = createdUtc == null ? 0 : long.Parse(createdUtc);
            var createdDate = createdDateLong.ToDateTimeFromEpoch();

            return new Comment()
            {
                Content = content,
                Author = author,
                DateCreated = createdDate
            };
        }

        #endregion

        /// <summary>
        /// We consider these errors handled because values are used in different context.
        /// It would have been better to have Web API models unified...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="errorEventArgs"></param>
        private void OnError(object sender, ErrorEventArgs errorEventArgs)
        {
            var memberName = errorEventArgs.ErrorContext.Member.ToString();
            if (memberName == "Ingredients" || memberName == "Thumbs")
            {
                errorEventArgs.ErrorContext.Handled = true;
            }
        }

        
    }
}