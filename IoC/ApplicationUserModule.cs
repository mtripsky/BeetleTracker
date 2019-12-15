using Autofac;
using BeetleTracker.Data;
using BeetleTracker.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace BeetleTracker.IoC
{
    public class ApplicationUserModule : Module
    {
        private readonly IConfiguration _config;

        public ApplicationUserModule(IConfiguration config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseClient<ApplicationUser>>()
                .As<IDatabaseClient<ApplicationUser>>()
                .WithParameter("connectionString", _config.GetSection("MongoDbSettings")["ConnectionString"])
                .WithParameter("dbName", _config.GetSection("MongoDbSettings")["DatabaseName"]);

            builder.RegisterType<EntityBaseRepository<ApplicationUser>>()
                .As<IEntityBaseRepository<ApplicationUser>>()
                .WithParameter("name", _config.GetSection("MongoDbSettings")["UsersCollectionName"]);
        }
    }
}
