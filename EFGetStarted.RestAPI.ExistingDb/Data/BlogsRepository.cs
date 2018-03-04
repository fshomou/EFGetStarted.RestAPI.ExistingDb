using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFGetStarted.RestAPI.ExistingDb.Models;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted.RestAPI.ExistingDb.Data
{
    public class BlogsRepository : IBlogRepository
    {
        private readonly BloggingContext _context;
        public BlogsRepository(BloggingContext context)

        {

            _context = context;

        }

        public void Add(Blog blog)
        {
            throw new NotImplementedException();
        }

        public Task<List<Blog>> GetAll() => _context.Blog.ToListAsync();

        public Task<List<Blog>> GetLatest(int num)
        {
            throw new NotImplementedException();
        }

        public Task<Blog> GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Blog blog)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges() => _context.SaveChangesAsync();
    }
}
