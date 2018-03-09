using System.Collections.Generic;

namespace EFGetStarted.RestAPI.ExistingDb.DTO
{
    public class BlogDto
    {
        public BlogDto()
        {
            PostDto = new HashSet<PostDto>();
        }

        public int BlogId { get; set; }
        public string Url { get; set; }

        public ICollection<PostDto> PostDto { get; set; }
    }
}