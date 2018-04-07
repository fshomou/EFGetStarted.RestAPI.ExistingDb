using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EntityFrameWorkUnitOfWork
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext

    {
        private Dictionary<Type, object> _repositories;
        private readonly TContext _context;

        public UnitOfWork(TContext context)

        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class

        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(_context);

            return (IRepository<TEntity>)_repositories[type];
        }

        public TContext DbContext {
            get
            {
                return this._context;
            }
        }

        public int SaveChanges()

        {
            
            try
            {
                // Attempt to save changes to the database
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return 0;
            }
        }

        public void Dispose()

        {
            _context?.Dispose();
        }
    }
}