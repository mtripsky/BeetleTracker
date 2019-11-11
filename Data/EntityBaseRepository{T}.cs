using System;
using System.Collections.Generic;
using System.Linq;
using BeetleTracker.Models;
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

        public void Delete(string id)
        {
            _collection.DeleteOne(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Find(p => true).ToList();
        }

        public T GetSingle(string id)
        {
            return _collection.Find(p => p.Id == id).FirstOrDefault();
        }

        public void Update(string id, T entity)
        {
            _collection.ReplaceOne(e => e.Id == id, entity);
        }
    }
}
