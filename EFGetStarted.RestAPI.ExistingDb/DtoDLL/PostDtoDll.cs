namespace EFGetStarted.RestAPI.ExistingDb.DtoDLL
{
    public class PostDtoDll
    {
        public int PostId { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public BlogDtoDll BlogDtoDll { get; set; }
    }
}