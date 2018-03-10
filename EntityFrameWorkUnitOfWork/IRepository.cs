using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameWorkUnitOfWork
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public interface IRepository<T> where T : class
    {
        T Add(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Delete(params T[] entities);

        void Delete(IEnumerable<T> entities);

        Task<IEnumerable<T>> Get();

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);

        IQueryable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

                                                           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, params Expression<Func<T, object>>[] includeExpressions);

        T GetFirstOrDefault(Expression<Func<T, bool>> predicate = null,
                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

                                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

                                 bool disableTracking = true);

        T Single(Expression<Func<T, bool>> predicate = null,

                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

            bool disableTracking = true);

        //TResult GetFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,

        // Expression<Func<T, bool>> predicate = null,

        // Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

        // Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

        // bool disableTracking = true);
        void Update(T entity);

        void Update(params T[] entities);

        void Update(IEnumerable<T> entities);

        IPagedList<T> GetPagedList(Expression<Func<T, bool>> predicate = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                      int pageIndex = 0,
                                      int pageSize = 20,
                                      bool disableTracking = true);

        Task<IPagedList<T>> GetPagedListAsync(Expression<Func<T, bool>> predicate = null,
                                                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                   Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                   int pageIndex = 0,
                                                   int pageSize = 20,
                                                   bool disableTracking = true,
                                                   CancellationToken cancellationToken = default(CancellationToken));

        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<T, TResult>> selector,
                                                            Expression<Func<T, bool>> predicate = null,
                                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                            int pageIndex = 0,
                                                            int pageSize = 20,
                                                            bool disableTracking = true,
                                                            CancellationToken cancellationToken = default(CancellationToken)) where TResult : class;

    }
}