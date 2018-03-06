using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using EFGetStarted.RestAPI.ExistingDb.Models;

namespace EFGetStarted.RestAPI.ExistingDb.UOW
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


        public T Add(T entity)

        {

            return _dbSet.Add(entity).Entity;

        }



        public void Delete(T entity)

        {

            var existing = this._dbSet.Find(entity);

            if (existing != null) this._dbSet.Remove(existing);

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

        public IQueryable<T> GetAll( params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> set = this._dbContext.Set<T>();

            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set;
        }

      

        public T Single(Expression<Func<T, bool>> predicate = null,

            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

            bool disableTracking = true)

        {

            IQueryable<T> query = this._dbSet;

            if (disableTracking) query = query.AsNoTracking();



            if (include != null) query = include(query);



            if (predicate != null) query = query.Where(predicate);



            if (orderBy != null)

                return orderBy(query).FirstOrDefault();

            
            return query.FirstOrDefault();

        }

        public IList<T> Get<TParamater>(IList<Expression<Func<T, TParamater>>> includeProperties)

        {
            IQueryable<T> query = this._dbSet;
            foreach (var include in includeProperties)
            {

                query = query.Include(include);
            }

            return query.ToList();
        }





        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)

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
        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate = null,

                                         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,

                                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,

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

                return orderBy(query).FirstOrDefault();

            }

            else

            {

                return query.FirstOrDefault();

            }

        }

    }
}
