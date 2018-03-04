using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFGetStarted.RestAPI.ExistingDb.Data;
using EFGetStarted.RestAPI.ExistingDb.Models;
using EFGetStarted.RestAPI.ExistingDb.GenericData;
using EFGetStarted.RestAPI.ExistingDb.UOW;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted.RestAPI.ExistingDb
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork

       where TContext : DbContext

    {

        private Dictionary<Type, object> _repositories;



        public UnitOfWork(TContext context)

        {

            Context = context ?? throw new ArgumentNullException(nameof(context));

        }



        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class

        {

            if (_repositories == null) _repositories = new Dictionary<Type, object>();



            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(Context);

            return (IRepository<TEntity>)_repositories[type];

        }



        public TContext Context { get; }



        public int SaveChanges()

        {

            return Context.SaveChanges();

        }



        public void Dispose()

        {

            Context?.Dispose();

        }

    }
}
