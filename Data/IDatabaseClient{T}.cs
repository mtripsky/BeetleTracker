using System;
using MongoDB.Driver;

namespace BeetleTracker.Data
{
    public interface IDatabaseClient<T>
    {
        IMongoCollection<T> GetCollection(string name);
    }
}
