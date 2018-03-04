﻿using Microsoft.EntityFrameworkCore;
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



        public void Add(T entity)

        {

            this._dbSet.Add(entity);

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
    }
}
