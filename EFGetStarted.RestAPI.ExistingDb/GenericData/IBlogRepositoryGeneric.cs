using EFGetStarted.RestAPI.ExistingDb.Data;
using EFGetStarted.RestAPI.ExistingDb.Models;

namespace EFGetStarted.RestAPI.ExistingDb.GenericData
{
    public interface IBlogRepositoryGeneric : IGenericRepository<Blog>
    {
        Blog Get(int blogId);
    }
}