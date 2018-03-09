using System;

namespace EntityFrameWorkUnitOfWork
{
    //https://github.com/threenine/Threenine.Data
    public interface IUnitOfWork : IDisposable

    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        int SaveChanges();
    }
}