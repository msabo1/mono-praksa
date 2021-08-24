using Library.App_Start;
using System.Web.Http;

namespace Library
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(AutofacContainerConfig.Register);
        }
    }
}
