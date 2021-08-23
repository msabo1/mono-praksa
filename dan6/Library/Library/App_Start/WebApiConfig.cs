using Autofac;
using Autofac.Integration.WebApi;
using Library.Service;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Web.Http;

namespace Library
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ValidateModelAttribute());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            // Autofac config
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<Mapper>().As<IMapper>();
            builder.RegisterModule<ServiceDIModule>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
