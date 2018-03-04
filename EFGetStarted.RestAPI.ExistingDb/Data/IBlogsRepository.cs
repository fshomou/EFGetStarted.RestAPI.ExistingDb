using EFGetStarted.RestAPI.ExistingDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFGetStarted.RestAPI.ExistingDb.Data
{
    public interface IBlogRepository
    {
        Task<List<Blog>> GetAll();

        Task<List<Blog>> GetLatest(int num);

        Task<Blog> GetOne(int id);

        void Add(Blog blog);

        void Remove(Blog blog);

        Task SaveChanges();
    }
}
