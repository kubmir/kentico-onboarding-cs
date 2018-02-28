using Notes.Api.Services.Dependency;
using Notes.Contracts.Dependency;
using Notes.Dependency.Containers;
using Notes.Repository.Dependency;

namespace Notes.Dependency.ContainersBuilders
{
    public static class DependencyContainerBuilder
    {
        public static IDependencyContainerResolver SetUpContainer()
        {
            var container = new DependencyContainer();

            container
                .RegisterDependency<RepositoryTypesBootstrapper>()
                .RegisterDependency<ApiServicesBootstrapper>();

            return container;
        }

        private static IDependencyContainerRegister RegisterDependency<T>(this IDependencyContainerRegister container) where T : IBootstrapper, new()
        {
            var bootstrapper = new T();
            return bootstrapper.RegisterType(container);
        }
    }
}
