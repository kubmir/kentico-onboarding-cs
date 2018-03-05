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
        public static IDependencyContainerResolver SetUpApiContainer(Func<IRouteManager> getRouteManager, Func<IConnectionStringManager> getConnectionString)
        {
            var container = new DependencyContainer();

            RegisterApiDependencies(getRouteManager, getConnectionString, container);

            return container;
        }

        internal static void RegisterApiDependencies(Func<IRouteManager> getRouteManager, Func<IConnectionStringManager> getConnectionString, IDependencyContainer container)
            => container
                .RegisterType(getRouteManager, LifetimeTypes.PerApplicationSingleton)
                .RegisterType(getConnectionString, LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IDependencyContainerResolver>(() => container, LifetimeTypes.PerApplicationSingleton)
                .RegisterDependency<RepositoryTypesBootstrapper>()
                .RegisterDependency<ApiServicesBootstrapper>()
                .RegisterDependency<ServicesTypesBootstrapper>();

        private static IDependencyContainerRegister RegisterDependency<T>(this IDependencyContainerRegister container) 
            where T : IBootstrapper, new()
        {
            var bootstrapper = new T();
            return bootstrapper.RegisterType(container);
        }
    }
}
