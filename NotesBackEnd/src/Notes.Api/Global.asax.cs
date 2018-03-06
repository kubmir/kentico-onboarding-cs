using System.Web.Http;

namespace Notes.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(DependencyInjectionConfig.Register);
        }
    }
}
