using EFGetStarted.RestAPI.ExistingDb.DTO;
using EFGetStarted.RestAPI.ExistingDb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFGetStarted.RestAPI.ExistingDb.DLL
{
    public class BlogManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public int AddBlog(BlogDto BlogDto)
        {
            Blog blog = new Blog();
            blog.Url = BlogDto.Url;

            foreach (var item in BlogDto.PostDto)   {
                Post post = new Post();
                post.Content = item.Content;
                blog.Post.Add(post);
            }



            // We can write one line of code if you like =>_unitOfWork.GetRepository<Blog>().Add(blog);
            UOW.IRepository<Blog> repBlog = this._unitOfWork.GetRepository<Blog>();
            blog = repBlog.Add(blog);

            this._unitOfWork.SaveChanges();
            return blog.BlogId;

        }

        public BlogDto GetBlog(int BlogId)
        {
            BlogDto blogDto = new BlogDto();
            // We can write one line of code if you like =>_unitOfWork.GetRepository<Blog>().Add(blog);
            UOW.IRepository<Blog> repBlog = this._unitOfWork.GetRepository<Blog>();


            //var blog = repBlog.Single(o=>o.BlogId == BlogId);


            var item = repBlog.GetFirstOrDefault(m => m.BlogId == BlogId, include: source => source.Include(m => m.Post));

            
            //blogDto.BlogId = blog.BlogId;
            //blogDto.Url = blog.Url;

            //foreach (var item in blog.Post)
            //{
            //    PostDto post = new PostDto();
            //    post.Content = item.Content;
            //    blogDto.PostDto.Add(post);
            //}



            return blogDto;

        }

    }
}
