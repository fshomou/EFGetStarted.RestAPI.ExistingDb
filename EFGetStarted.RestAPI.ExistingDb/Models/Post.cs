using System;
using System.Collections.Generic;

namespace EFGetStarted.RestAPI.ExistingDb.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
        }

        public int PostId { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public Blog Blog { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }
}
