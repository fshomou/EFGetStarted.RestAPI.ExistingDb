namespace EFGetStarted.RestAPI.ExistingDb.Models
{
    public partial class Comment
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public Post Post { get; set; }
    }
}