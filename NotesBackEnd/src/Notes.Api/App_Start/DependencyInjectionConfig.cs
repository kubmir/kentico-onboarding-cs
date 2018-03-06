using System.Web.Http;
using System.Web.Http.Dependencies;
using Notes.Api.Routes;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;
using Notes.Dependency.ContainersBuilders;

namespace Notes.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            IDependencyContainerResolver container = DependencyContainerBuilder.SetUpApiContainer(GetRouteManager);
            config.DependencyResolver = (IDependencyResolver) container.Resolve(typeof(IDependencyResolver));
        }

        private static IRouteManager GetRouteManager()
            => new RouteManager();
    }
}