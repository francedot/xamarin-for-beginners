using System;

namespace MyReddit.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Subreddit { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public int Likes { get; set; }
        public int CommentsCount { get; set; }
        public string ThumbnailUri { get; set; }
    }
}