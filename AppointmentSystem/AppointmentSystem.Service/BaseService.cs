using AppointmentSystem.Data.Entities;
using AppointmentSystem.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AppointmentSystem.Service
{
    public class BaseService<T> : IBaseService<T> where T : IBaseEntity, new()
    {
        protected IBaseRepository<T> baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public void Delete(T item)
        {
            baseRepository.Delete(item);
        }

        public void DeleteById(int id)
        {
            baseRepository.DeleteById(id);
        }

        public void Edit(T item)
        {
            baseRepository.Edit(item);
        }

        public void EditById(int id, T item)
        {
            baseRepository.EditById(id, item);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return baseRepository.GetAll(filter);
        }

        public T GetById(int id)
        {
            return baseRepository.GetById(id);
        }

        public virtual void Insert(T item)
        {
            baseRepository.Insert(item);
        }
    }
}
