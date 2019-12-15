using System;
using MongoDB.Driver;

namespace BeetleTracker.Data
{
    public class DatabaseClient<T> : IDatabaseClient<T>
    {
        private readonly IMongoDatabase database;

        public DatabaseClient(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            this.database = client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection(string name) => database.GetCollection<T>(name);
    }
}
