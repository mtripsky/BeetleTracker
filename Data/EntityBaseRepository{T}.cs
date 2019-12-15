using System;
using System.Collections.Generic;
using System.Linq;
using BeetleTracker.Models.Entities;
using MongoDB.Driver;

namespace BeetleTracker.Data
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
        where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public EntityBaseRepository(IDatabaseClient<T> client, string name)
        {
            _collection = client.GetCollection(name);
        }

        public int Count()
        {
            return GetAll().Count();
        }

        public T Create(T entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _collection.DeleteOne(e => e.Id == entity.Id);
        }

        public void Delete(Guid id)
        {
            _collection.DeleteOne(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Find(p => true).ToList();
        }

        public T GetSingle(Guid id)
        {
            return _collection.Find(p => p.Id == id).FirstOrDefault();
        }

        public T GetSingle(Func<T, bool> predicate)
        {
            return _collection.AsQueryable().FirstOrDefault(predicate);
        }

        public void Update(Guid id, T entity)
        {
            _collection.ReplaceOne(e => e.Id == id, entity);
        }
    }
}
