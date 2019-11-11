using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BeetleTracker.Data
{
    public interface IEntityBaseRepository<T>
    {
        IEnumerable<T> GetAll();

        int Count();

        T GetSingle(string id);

        T Create(T entity);

        void Update(string id, T entity);

        void Delete(T entity);

        void Delete(string id);
    }
}
