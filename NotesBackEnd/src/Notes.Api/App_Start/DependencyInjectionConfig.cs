using System.Web.Http;
using Notes.Api.Services.Helpers;
using Notes.Contracts.Dependency;
using Notes.Dependency.Containers;

namespace Notes.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            IDependencyContainer container = new DependencyContainer()
                .RegisterDependency(new Repository.Dependency.RepositoryTypesRegistration())
                .RegisterDependency(new Dependency.ApiTypesRegistration());

            config.DependencyResolver = new ContainerResolver(container);
        }

        private static IDependencyContainer RegisterDependency(this IDependencyContainer container, IBootstrapper registrationClass)
            => registrationClass.RegisterType(container);
    }
}