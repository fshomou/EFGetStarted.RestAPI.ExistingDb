using EFGetStarted.RestAPI.ExistingDb.Data;
using EFGetStarted.RestAPI.ExistingDb.Models;
using EFGetStarted.RestAPI.ExistingDb.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFGetStarted.RestAPI.ExistingDb
{
    //https://github.com/threenine/Threenine.Data
    public interface IUnitOfWork : IDisposable

    {

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        int SaveChanges();

    }



   
}
