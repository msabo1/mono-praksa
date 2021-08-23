using Autofac;
using Library.Repository;
using Library.Service.Common;

namespace Library.Service
{
    public class ServiceDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorsService>().As<IAuthorsService>();
            builder.RegisterType<BooksService>().As<IBooksService>();
            builder.RegisterModule<RepositoryDIModule>();
        }
    }
}
