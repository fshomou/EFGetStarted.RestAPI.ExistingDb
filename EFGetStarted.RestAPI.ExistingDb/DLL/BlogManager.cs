using EFGetStarted.RestAPI.ExistingDb.DTO;
using EFGetStarted.RestAPI.ExistingDb.Models;
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
            _unitOfWork = unitOfWork;
        }

        public void AddBlog(BlogDto BlogDto)
        {
            Blog blog = new Blog();
            blog.Url = BlogDto.Url;

            _unitOfWork.GetRepository<Blog>().Add(blog);
            _unitOfWork.SaveChanges();
        }  
       
    }
}
