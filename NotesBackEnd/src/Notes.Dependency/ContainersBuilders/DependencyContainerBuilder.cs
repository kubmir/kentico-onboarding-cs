using System;
using Notes.Api.Services.Dependency;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;
using Notes.Dependency.Containers;
using Notes.Repository.Dependency;

namespace Notes.Dependency.ContainersBuilders
{
    public static class DependencyContainerBuilder
    {
        public static IDependencyContainerResolver SetUpApiContainer(Func<IRouteManager> getRouteManager)
        {
            var container = new DependencyContainer();

            RegisterApiDependencies(getRouteManager, container);

            return container;
        }

        internal static void RegisterApiDependencies(Func<IRouteManager> getRouteManager, IDependencyContainer container)
           => container
                .RegisterType(getRouteManager, LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IDependencyContainerResolver>(() => container, LifetimeTypes.PerApplicationSingleton)
                .RegisterDependency<RepositoryTypesBootstrapper>()
                .RegisterDependency<ApiServicesBootstrapper>();

        private static IDependencyContainerRegister RegisterDependency<T>(this IDependencyContainerRegister container) 
            where T : IBootstrapper, new()
        {
            var bootstrapper = new T();
            return bootstrapper.RegisterType(container);
        }
    }
}
