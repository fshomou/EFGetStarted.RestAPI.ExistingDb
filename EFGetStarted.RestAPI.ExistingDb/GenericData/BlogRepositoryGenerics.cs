using EFGetStarted.RestAPI.ExistingDb.Data;
using EFGetStarted.RestAPI.ExistingDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace EFGetStarted.RestAPI.ExistingDb.GenericData
{
    public class BlogRepositoryGenerics : GenericRepository<Blog>, IBlogRepositoryGeneric
    {
        public BlogRepositoryGenerics(DataContext context) : base(context)
        {
        }
        public Blog Get(int blogId)
        {
            var query = GetAll().FirstOrDefault(b => b.BlogId == blogId);
            return query;
        }

        public async Task<Blog> GetSingleAsyn(int blogId)
        {
            return await _context.Set<Blog>().FindAsync(blogId);
        }

        public override Blog Update(Blog t, object key)
        {
            Blog exist = _context.Set<Blog>().Find(key);
            if (exist != null)
            {
                //t.CreatedBy = exist.CreatedBy;
                //t.CreatedOn = exist.CreatedOn;
            }
            return base.Update(t, key);
        }

        public async override Task<Blog> UpdateAsyn(Blog t, object key)
        {
            Blog exist = await _context.Set<Blog>().FindAsync(key);
            if (exist != null)
            {
                //t.CreatedBy = exist.CreatedBy;
                //t.CreatedOn = exist.CreatedOn;
            }
            return await base.UpdateAsyn(t, key);
        }
    }
}
