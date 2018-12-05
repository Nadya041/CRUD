using AppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AppointmentSystem.Service
{
    public interface IBaseService<T> where T : IBaseEntity, new()
    {
        T GetById(int id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
        void Insert(T item);
        void Edit(T item);
        void EditById(int id, T item);
        void Delete(T item);
        void DeleteById(int id);
    }
}
