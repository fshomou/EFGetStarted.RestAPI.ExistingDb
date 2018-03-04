using EFGetStarted.RestAPI.ExistingDb.Data;
using EFGetStarted.RestAPI.ExistingDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace EFGetStarted.RestAPI.ExistingDb.GenericData
{
    public interface IBlogRepositoryGeneric : IGenericRepository<Blog>
    {
        Blog Get(int blogId);
    }
}
