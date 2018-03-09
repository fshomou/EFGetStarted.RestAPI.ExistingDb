using EFGetStarted.RestAPI.ExistingDb.DTO;
using EFGetStarted.RestAPI.ExistingDb.DtoDLL;
using EFGetStarted.RestAPI.ExistingDb.Models;
using EntityFrameWorkUnitOfWork;
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

        public int AddBlog(BlogDtoDll BlogDtoDll)
        {
            Blog blog = new Blog();
            blog.Url = BlogDtoDll.Url;

            foreach (var item in BlogDtoDll.PostDtoDll)   {
                Post post = new Post();
                post.Content = item.Content;
                post.Title = item.Title;
                blog.Post.Add(post);
            }



            // We can write one line of code if you like =>_unitOfWork.GetRepository<Blog>().Add(blog);
            IRepository<Blog> repBlog = this._unitOfWork.GetRepository<Blog>();
            blog = repBlog.Add(blog);

            this._unitOfWork.SaveChanges();
            return blog.BlogId;

        }

        public BlogDtoDll GetBlog(int BlogId)
        {
            BlogDtoDll BlogDtoDll = new BlogDtoDll();
            // We can write one line of code if you like =>_unitOfWork.GetRepository<Blog>().Add(blog);
            IRepository<Blog> repBlog = this._unitOfWork.GetRepository<Blog>();


            //var blog = repBlog.Single(o=>o.BlogId == BlogId);


            Blog blog = repBlog.GetFirstOrDefault(m => m.BlogId == BlogId, include: source => source.Include(m => m.Post));

            BlogDtoDll.BlogId = blog.BlogId;
            BlogDtoDll.Url = blog.Url;


            foreach (var item in blog.Post)
            {
                PostDtoDll post = new PostDtoDll();
                post.Content = item.Content;
                BlogDtoDll.PostDtoDll.Add(post);
            }



            return BlogDtoDll;

        }

    }
}
