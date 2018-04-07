using EFGetStarted.RestAPI.ExistingDb.DLL;
using EFGetStarted.RestAPI.ExistingDb.DTO;
using EFGetStarted.RestAPI.ExistingDb.DtoDLL;
using EFGetStarted.RestAPI.ExistingDb.Models;
using EntityFrameWorkUnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFGetStarted.RestAPI.ExistingDb.Controllers
{
    [Produces("application/json")]
    [Route("api/BlogsUOW")]
    public class BlogsUOWController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<BlogsUOWController> _logger;

        //public BlogsUOWController(IUnitOfWork unitOfWork, ILogger<BlogsUOWController> logger)
        //{
        //	this._unitOfWork = unitOfWork;
        //	this._logger = logger;
        //}

        public BlogsUOWController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        // GET: api/Blogs
        [HttpGet]
        public List<BlogDto>  GetAllBlogs()
        {
            BlogManager blogManager = new BlogManager(this._unitOfWork);

            List<BlogDto> blogDtolsit = new List<BlogDto>();
            var bloglist = blogManager.GetBlogAll();
            foreach (BlogDtoDll blog in bloglist)
            {
                BlogDto blogDto = new BlogDto
                {
                    BlogId = blog.BlogId,
                    Url = blog.Url
                };

                List<PostDto> postDtolist = new List<PostDto>();

                foreach (var post in blog.PostDtoDll)
                {
                    PostDto postDto = new PostDto
                    {
                        BlogId = post.BlogId,
                        Content = post.Content,
                        Title = post.Title,
                        PostId = post.PostId
                    };
                    postDtolist.Add(postDto);
                }
                blogDto.PostDto = postDtolist;
                blogDtolsit.Add(blogDto);

            }

            return blogDtolsit;
     
        }

        [HttpPost]
        public IActionResult PostBlog([FromBody]BlogDto BlogDto)
        {
            int blog_id = 0;

            if (ModelState.IsValid)
            {
                BlogDtoDll BlogDtoDll = new BlogDtoDll();
                BlogDtoDll.Url = BlogDto.Url;

                foreach (var item in BlogDto.PostDto)
                {
                    PostDtoDll post = new PostDtoDll();
                    post.Content = item.Content;
                    post.Title = item.Title;
                    BlogDtoDll.PostDtoDll.Add(post);
                }

                BlogManager blogManager = new BlogManager(this._unitOfWork);

                blog_id = blogManager.AddBlog(BlogDtoDll);
            }

            return CreatedAtAction(actionName: "GetBlog", routeValues: new { id = blog_id }, value: null);
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public IActionResult GetBlog([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                BlogManager blogManager = new BlogManager(this._unitOfWork);

                BlogDtoDll blogDtoDll = blogManager.GetBlog(id);
                BlogDto blogDto = new BlogDto
                {
                    BlogId = blogDtoDll.BlogId,
                    Url = blogDtoDll.Url
                };

                List<PostDto> postDtolist = new List<PostDto>();

                foreach (var post in blogDtoDll.PostDtoDll)
                {
                    PostDto postDto = new PostDto
                    {
                        BlogId = post.BlogId,
                        Content = post.Content,
                        Title = post.Title,
                        PostId = post.PostId
                    };
                    postDtolist.Add(postDto);
                }
                blogDto.PostDto = postDtolist;

                if (blogDto != null) return Ok(blogDto);
                return StatusCode(404);
            }
            else
            {
                return StatusCode(400);
            }



            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //var blog = await this._unitOfWork.GetRepository<Blog>().Single(m => m.BlogId == id);
            ////var blog = await _context.Blog.SingleOrDefaultAsync(m => m.BlogId == id);

            //if (blog == null)
            //{
            //    return NotFound();
            //}

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                BlogManager blogManager = new BlogManager(this._unitOfWork);

                if (blogManager.DeleteBlog(id) != 0)
                {
                    return StatusCode(204);
                }
                else
                {
                    return StatusCode(501);
                }
                
            }

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //var blog = await this._unitOfWork.GetRepository<Blog>().Single(m => m.BlogId == id);
            ////var blog = await _context.Blog.SingleOrDefaultAsync(m => m.BlogId == id);

            //if (blog == null)
            //{
            //    return NotFound();
            //}

            return Ok();
        }

        //// PUT: api/Blogs/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBlog([FromRoute] int id, [FromBody] Blog blog)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != blog.BlogId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(blog).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BlogExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Blogs
        //[HttpPost]
        //public async Task<IActionResult> PostBlog([FromBody] Blog blog)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    this._unitOfWork.GetRepository<Blog>().Add(blog);
        //    this._unitOfWork.SaveChanges();

        //    return  CreatedAtAction(actionName: "GetBlog", routeValues: new { id = blog.BlogId }, value: blog);
        //}

        //// DELETE: api/Blogs/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBlog([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var blog = await _context.Blog.SingleOrDefaultAsync(m => m.BlogId == id);
        //    if (blog == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Blog.Remove(blog);
        //    await _context.SaveChangesAsync();

        //    return Ok(blog);
        //}

        //private bool BlogExists(int id)
        //{
        //    return _context.Blog.Any(e => e.BlogId == id);
        //}
    }
}