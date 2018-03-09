using System.Collections.Generic;

namespace EFGetStarted.RestAPI.ExistingDb.DtoDLL
{
    public class BlogDtoDll
    {
        public BlogDtoDll() => PostDtoDll = new HashSet<PostDtoDll>();

        public int BlogId { get; set; }
        public string Url { get; set; }

        public ICollection<PostDtoDll> PostDtoDll { get; set; }
    }
}