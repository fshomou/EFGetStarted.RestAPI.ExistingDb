using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeExpressions);

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
    }
}