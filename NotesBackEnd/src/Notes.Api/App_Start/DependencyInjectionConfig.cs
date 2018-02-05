using System.Web.Http;
using Notes.Api.Services.Services;
using Notes.Contracts.Dependency;
using Notes.Dependency.ContainersBuilders;

namespace Notes.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            IDependencyContainer container = DependencyContainerBuilder.SetUpContainer();

            config.DependencyResolver = new ContainerResolver(container);
        }
    }
}