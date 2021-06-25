using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string Bio { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();
        public ICollection<Retweet> Retweets { get; set; } = new List<Retweet>();
        public ICollection<SavedPost> UserSavedPosts { get; set; } = new List<SavedPost>();
        public ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
    }
}