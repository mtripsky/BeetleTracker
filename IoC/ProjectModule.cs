using Autofac;
using BeetleTracker.Data;
using BeetleTracker.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace BeetleTracker.IoC
{
    public class ProjectModule : Module
    {
        private readonly IConfiguration _config;

        public ProjectModule(IConfiguration config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseClient<Project>>()
                .As<IDatabaseClient<Project>>()
                .WithParameter("connectionString", _config.GetSection("MongoDbSettings")["ConnectionString"])
                .WithParameter("dbName", _config.GetSection("MongoDbSettings")["DatabaseName"]);

            builder.RegisterType<EntityBaseRepository<Project>>()
                .As<IEntityBaseRepository<Project>>()
                .WithParameter("name", _config.GetSection("MongoDbSettings")["ProjectsCollectionName"]);
        }
    }
}
