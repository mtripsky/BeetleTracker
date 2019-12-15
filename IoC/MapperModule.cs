using Autofac;
using AutoMapper;
using BeetleTracker.Models.Mappers;
using System.Collections.Generic;

namespace BeetleTracker.IoC
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProjectProfile>().As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf()
               .SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>()
                .CreateMapper(ctx.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
