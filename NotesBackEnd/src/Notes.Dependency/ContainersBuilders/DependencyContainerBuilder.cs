using System;
using Notes.Api.Services.Dependency;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;
using Notes.Dependency.Containers;
using Notes.Repository.Dependency;
using Notes.Services.Dependency;

namespace Notes.Dependency.ContainersBuilders
{
    public static class DependencyContainerBuilder
    {
        public static IDependencyContainerResolver SetUpApiContainer(Func<IRouteOptions> getRouteOptions)
        {
            var container = new DependencyContainer();

            RegisterApiDependencies(getRouteOptions, container);

            return container;
        }

        internal static void RegisterApiDependencies(Func<IRouteOptions> getRouteOptions, IDependencyContainer container)
            => container
                .RegisterType(getRouteOptions, LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IDependencyContainerResolver>(() => container, LifetimeTypes.PerApplicationSingleton)
                .RegisterDependency<RepositoryTypesBootstrapper>()
                .RegisterDependency<ApiServicesBootstrapper>()
                .RegisterDependency<ServicesTypesBootstrapper>();

        private static IDependencyContainerRegister RegisterDependency<TBootstrapper>(this IDependencyContainerRegister container) 
            where TBootstrapper : IBootstrapper, new()
                => new TBootstrapper().RegisterType(container);
    }
}
