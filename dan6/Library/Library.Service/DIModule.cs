using Autofac;
using Library.Service.Common;

namespace Library.Service
{
    public class DIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorsService>().As<IAuthorsService>();
            builder.RegisterType<BooksService>().As<IBooksService>();
        }
    }
}
