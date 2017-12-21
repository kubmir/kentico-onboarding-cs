using System.Web.Http;
using Notes.Api.Services.Dependency;
using Notes.Api.Services.Services;
using Notes.Contracts.Dependency;
using Notes.Dependency.Containers;
using Notes.Repository.Dependency;
using Notes.Services.Dependency;

namespace Notes.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            IDependencyContainer container = new DependencyContainer()
                .RegisterDependency(new ServicesTypesRegistration())
                .RegisterDependency(new RepositoryTypesBootstrapper())
                .RegisterDependency(new ApiServicesBootstrapper());


            config.DependencyResolver = new ContainerResolver(container);
        }

        private static IDependencyContainer RegisterDependency(this IDependencyContainer container, IBootstrapper bootstrapper)
            => bootstrapper.RegisterType(container);
    }
}