using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFGetStarted.RestAPI.ExistingDb.Models;
using EFGetStarted.RestAPI.ExistingDb.Data;
using EFGetStarted.RestAPI.ExistingDb.DTO;
using EFGetStarted.RestAPI.ExistingDb.DLL;

namespace EFGetStarted.RestAPI.ExistingDb.Controllers
{
    [Produces("application/json")]
    [Route("api/BlogsUOW")]
    public class BlogsUOWController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogsUOWController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<IEnumerable<Blog>> GetBlogs()
        {
            return await this._unitOfWork.GetRepository<Blog>().Get();
        }

        [HttpPost]
        public async Task<IActionResult> PostBlog([FromBody]BlogDto BlogDto)
        {

            if (BlogDto != null)
            {
                BlogManager blogManager = new BlogManager(_unitOfWork);

                blogManager.AddBlog(BlogDto);
            }

            return CreatedAtAction(actionName: "GetBlog", routeValues: new { id = BlogDto.BlogId }, value: BlogDto);


        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public IActionResult GetBlog([FromRoute] int id)
        {
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