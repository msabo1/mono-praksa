using Autofac;
using Library.Repository.Common;
using System.Configuration;
using System.Data.SqlClient;

namespace Library.Repository
{
    public class RepositoryDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorsRepository>().As<IAuthorsRepository>();
            builder.RegisterType<BooksRepository>().As<IBooksRepository>();
            builder
                .Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["Library"].ConnectionString))
                .As<SqlConnection>()
                .InstancePerRequest();
        }
    }
}
