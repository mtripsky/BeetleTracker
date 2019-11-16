using System;
using Autofac;
using BeetleTracker.Data;
using BeetleTracker.Models;
using Microsoft.Extensions.Configuration;

namespace BeetleTracker.IoC
{
    public class IssueModule : Module
    {
        private readonly IConfiguration _config;

        public IssueModule(IConfiguration config)
        {
            _config = config;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseClient<Issue>>()
                .As<IDatabaseClient<Issue>>()
                .WithParameter("connectionString", _config.GetConnectionString("Connection"))
                .WithParameter("dbName", _config.GetConnectionString("IssueDatabaseName"));

            builder.RegisterType<EntityBaseRepository<Issue>>()
                .As<IEntityBaseRepository<Issue>>()
                .WithParameter("name", _config.GetConnectionString("IssuesCollectionName"));
        }
    }
}
