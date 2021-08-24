using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Library.Mapper;
using System.Reflection;
using System.Web.Http;

namespace Library.App_Start
{
    public class AutofacContainerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<Service.DIModule>();
            builder.RegisterModule<Repository.DIModule>();

            builder.Register(c => new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<Repository.RepositoryProfile>();
                    cfg.AddProfile<RestProfile>();
                }
            )).As<IConfigurationProvider>().SingleInstance();

            builder.Register(c => new AutoMapper.Mapper(c.Resolve<IConfigurationProvider>())).As<IMapper>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
