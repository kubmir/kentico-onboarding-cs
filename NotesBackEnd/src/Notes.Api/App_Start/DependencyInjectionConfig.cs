using System.Web.Http;
using System.Web.Http.Dependencies;
using Notes.Api.Routes;
using Notes.Contracts.ApiServices;
using Notes.Dependency.ContainersBuilders;

namespace Notes.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            var container = DependencyContainerBuilder.SetUpApiContainer(GetRouteOptions);
            config.DependencyResolver = container.Resolve<IDependencyResolver>();
        }

        private static IRouteOptions GetRouteOptions()
            => new RouteOptions();
    }
}