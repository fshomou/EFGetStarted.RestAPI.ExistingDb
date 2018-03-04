using EFGetStarted.RestAPI.ExistingDb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFGetStarted.RestAPI.ExistingDb.UOW
{
    public interface IRepositoryFactory

    {

        IRepository<T> GetRepository<T>() where T : class;

    }
}
