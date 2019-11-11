using System;
using Autofac;
using BeetleTracker.Data;
using BeetleTracker.Models;
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
                .WithParameter("connectionString", _config.GetConnectionString("Connection"))
                .WithParameter("dbName", _config.GetConnectionString("ProjectDatabaseName"));

            builder.RegisterType<EntityBaseRepository<Project>>()
                .As<IEntityBaseRepository<Project>>()
                .WithParameter("name", _config.GetConnectionString("ProjectsCollectionName"));
        }
    }
}
