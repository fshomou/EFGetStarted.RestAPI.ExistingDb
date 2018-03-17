using EFGetStarted.RestAPI.ExistingDb.Data;
using EFGetStarted.RestAPI.ExistingDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFGetStarted.RestAPI.ExistingDb.Controllers
{
    [Produces("application/json")]
    [Route("api/Blogs")]
    public class BlogsController : Controller
    {
        private readonly IBlogRepository _BlogsRepository;
        private readonly ILogger _logger;

        public BlogsController(IBlogRepository blogsRepository, ILogger<BlogsController> logger)
        {
            _BlogsRepository = blogsRepository;
            _logger = logger;

        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<IEnumerable<Blog>> GetBlogs()
        {
            _logger.LogInformation("GetBlogs");

            return await this._BlogsRepository.GetAll();
        }

        //// GET: api/Blogs/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetBlog([FromRoute] int id)
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

        //    return Ok(blog);
        //}

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

        //// POST: api/Blogs
        //[HttpPost]
        //public async Task<IActionResult> PostBlog([FromBody] Blog blog)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Blog.Add(blog);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
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