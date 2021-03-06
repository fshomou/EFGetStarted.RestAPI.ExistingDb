﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameWorkUnitOfWork
{
    public class Repository<T> : IRepository<T> where T : class

    {
        protected readonly DbContext _dbContext;

        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context)

        {
            this._dbContext = context;

            this._dbSet = this._dbContext.Set<T>();
        }

        //public void Add(T entity)

        //{
        //    _dbSet.Add(entity);

        //}

        public IEnumerable<T> GetAll()
        {
            //return _dbSet.Include("Post.Comment");
            return _dbSet.Include("Post");
            //return _dbSet;
        }

        public T Add(T entity)

        {
            return _dbSet.Add(entity).Entity;
        }

        public void Delete(T entity)

        {
            //var existing = this._dbSet.Find(entity);

            this._dbSet.Remove(entity);
        }

        public void Delete(object id)

        {
            var typeInfo = typeof(T).GetTypeInfo();

            var key = this._dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();

            var property = typeInfo.GetProperty(key?.Name);

            if (property != null)

            {
                var entity = Activator.CreateInstance<T>();

                property.SetValue(entity, id);

                this._dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else

            {
                var entity = this._dbSet.Find(id);

                if (entity != null) Delete(entity);
            }
        }

        public void Delete(params T[] entities)

        {
            this._dbSet.RemoveRange(entities);
        }

        public void Delete(IEnumerable<T> entities)

        {
            this._dbSet.RemoveRange(entities);
        }

        public IQueryable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

                                                           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = this._dbContext.Set<T>();

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set;
        }

        //public T Single(Expression<Func<T, bool>> predicate = null,

        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

        //    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

        //    bool disableTracking = true)

        //{
        //    IQueryable<T> query = this._dbSet;

        //    if (disableTracking) query = query.AsNoTracking();

        //    if (include != null) query = include(query);

        //    if (predicate != null) query = query.Where(predicate);

        //    if (orderBy != null)

        //        return orderBy(query).FirstOrDefault();

        //    return query.FirstOrDefault();
        //}

        public IList<T> Get<TParamater>(IList<Expression<Func<T, TParamater>>> includeProperties)

        {
            IQueryable<T> query = this._dbSet;
            foreach (var include in includeProperties)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)

        {
            return this._dbSet.Where(predicate).AsEnumerable();
        }

        public void Update(T entity)

        {
            this._dbSet.Update(entity);
        }

        public void Update(params T[] entities)

        {
            this._dbSet.UpdateRange(entities);
        }

        public void Update(IEnumerable<T> entities)

        {
            this._dbSet.UpdateRange(entities);
        }

        public Task<IEnumerable<T>> Get()
        {
            return Task.FromResult(_dbSet.ToAsyncEnumerable().ToEnumerable<T>());
        }

        // TODO
        //public Task<T> Single(Expression<Func<T, bool>> predicate = null,

        //   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

        //   Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

        //   bool disableTracking = true)

        //{
        //    IQueryable<T> query = this._dbSet;

        //    if (disableTracking) query = query.AsNoTracking();

        //    if (include != null) query = include(query);

        //    if (predicate != null) query = query.Where(predicate);

        //    if (orderBy != null) return Task.FromResult(orderBy(query).FirstOrDefault());

        //    return Task.FromResult(query.FirstOrDefault());

        //}

        //public TResult GetFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,

        //                                          Expression<Func<T, bool>> predicate = null,

        //                                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

        //                                          Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

        //                                          bool disableTracking = true)

        //{
        //    IQueryable<T> query = _dbSet;

        //    if (disableTracking)

        //    {
        //        query = query.AsNoTracking();

        //    }

        //    if (include != null)

        //    {
        //        query = include(query);

        //    }

        //    if (predicate != null)

        //    {
        //        query = query.Where(predicate);

        //    }

        //    if (orderBy != null)

        //    {
        //        return orderBy(query).Select(selector).FirstOrDefault();

        //    }

        //    else

        //    {
        //        return query.Select(selector).FirstOrDefault();

        //    }

        //}
        //public T GetFirstOrDefault(Expression<Func<T, bool>> predicate = null,

        //                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

        //                                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

        //                                 bool disableTracking = true)

        //{
        //    IQueryable<T> query = _dbSet;

        //    if (disableTracking)

        //    {
        //        query = query.AsNoTracking();
        //    }

        //    if (include != null)

        //    {
        //        query = include(query);
        //    }

        //    if (predicate != null)

        //    {
        //        query = query.Where(predicate);
        //    }

        //    if (orderBy != null)

        //    {
        //        return orderBy(query).FirstOrDefault();
        //    }
        //    else

        //    {
        //        return query.FirstOrDefault();
        //    }
        //}

        public IPagedList<T> GetPagedList(Expression<Func<T, bool>> predicate = null,

                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

                                                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

                                                int pageIndex = 0,

                                                int pageSize = 20,

                                                bool disableTracking = true)

        {

            IQueryable<T> query = _dbSet;

            if (disableTracking)

            {

                query = query.AsNoTracking();

            }



            if (include != null)

            {

                query = include(query);

            }



            if (predicate != null)

            {

                query = query.Where(predicate);

            }



            if (orderBy != null)

            {

                return orderBy(query).ToPagedList(pageIndex, pageSize);

            }

            else

            {

                return query.ToPagedList(pageIndex, pageSize);

            }

        }

        public Task<IPagedList<T>> GetPagedListAsync(Expression<Func<T, bool>> predicate = null,

                                                           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

                                                           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

                                                           int pageIndex = 0,

                                                           int pageSize = 20,

                                                           bool disableTracking = true,

                                                           CancellationToken cancellationToken = default(CancellationToken))

        {

            IQueryable<T> query = _dbSet;

            if (disableTracking)

            {

                query = query.AsNoTracking();

            }



            if (include != null)

            {

                query = include(query);

            }



            if (predicate != null)

            {

                query = query.Where(predicate);

            }



            if (orderBy != null)

            {

                return orderBy(query).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);

            }

            else

            {

                return query.ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);

            }

        }



        /// <summary>

        /// Gets the <see cref="IPagedList{TResult}"/> based on a predicate, orderby delegate and page information. This method default no-tracking query.

        /// </summary>

        /// <param name="selector">The selector for projection.</param>

        /// <param name="predicate">A function to test each element for a condition.</param>

        /// <param name="orderBy">A function to order elements.</param>

        /// <param name="include">A function to include navigation properties</param>

        /// <param name="pageIndex">The index of page.</param>

        /// <param name="pageSize">The size of the page.</param>

        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>

        /// <returns>An <see cref="IPagedList{TResult}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>

        /// <remarks>This method default no-tracking query.</remarks>

        public IPagedList<TResult> GetPagedList<TResult>(Expression<Func<T, TResult>> selector,

                                                         Expression<Func<T, bool>> predicate = null,

                                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

                                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

                                                         int pageIndex = 0,

                                                         int pageSize = 20,

                                                         bool disableTracking = true)

            where TResult : class

        {

            IQueryable<T> query = _dbSet;

            if (disableTracking)

            {

                query = query.AsNoTracking();

            }



            if (include != null)

            {

                query = include(query);

            }



            if (predicate != null)

            {

                query = query.Where(predicate);

            }



            if (orderBy != null)

            {

                return orderBy(query).Select(selector).ToPagedList(pageIndex, pageSize);

            }

            else

            {

                return query.Select(selector).ToPagedList(pageIndex, pageSize);

            }

        }

        public Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<T, TResult>> selector,

                                                                    Expression<Func<T, bool>> predicate = null,

                                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

                                                                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

                                                                    int pageIndex = 0,

                                                                    int pageSize = 20,

                                                                    bool disableTracking = true,

                                                                    CancellationToken cancellationToken = default(CancellationToken))

            where TResult : class

        {

            IQueryable<T> query = _dbSet;

            if (disableTracking)

            {

                query = query.AsNoTracking();

            }



            if (include != null)

            {

                query = include(query);

            }



            if (predicate != null)

            {

                query = query.Where(predicate);

            }



            if (orderBy != null)

            {

                return orderBy(query).Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);

            }

            else

            {

                return query.Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);

            }

        }

        public IQueryable<T> Query()
        {
            return this._dbSet.AsQueryable();
        }
    }
}