using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BeetleTracker.Data
{
    public interface IEntityBaseRepository<T>
    {
        IEnumerable<T> GetAll();

        int Count();

        T GetSingle(Guid id);

        T GetSingle(Func<T, bool> predicate);

        T Create(T entity);

        void Update(Guid id, T entity);

        void Delete(T entity);

        void Delete(Guid id);
    }
}
