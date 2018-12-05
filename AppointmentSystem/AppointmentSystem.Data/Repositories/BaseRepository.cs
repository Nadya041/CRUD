using Microsoft.EntityFrameworkCore;
using AppointmentSystem.Data.Context;
using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AppointmentSystem.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        protected DbContext Context { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public BaseRepository(AppointmentSystemContext dbContext)
        {
            Context = dbContext;
            DbSet = Context.Set<T>();
        }

        public virtual void Delete(T item)
        {
            Context.Entry(item).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public virtual void Edit(T item)
        {
            DbSet.Update(item);
            Context.SaveChanges();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
                query = query.Where(filter);  

            return query.ToList();
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(T item)
        {
            Context.Add(item);
            Context.SaveChanges();
        }

        public virtual void DeleteById(int id)
        {
            T item = GetById(id);
            Context.Entry(item).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public virtual void EditById(int id, T item)
        {
            T entity = GetById(id);           
            DbSet.Update(entity);

            Context.SaveChanges();
        }
    }
}
