using Notes.Api.Services.Dependency;
using Notes.Contracts.Dependency;
using Notes.Dependency.Containers;
using Notes.Repository.Dependency;
using Notes.Services.Dependency;

namespace Notes.Dependency.ContainersBuilders
{
    public static class DependencyContainerBuilder
    {
        public static IDependencyContainerResolver SetUpContainer()
        {
            var container = new DependencyContainer();

            container
                .RegisterDependency(new RepositoryTypesBootstrapper())
                .RegisterDependency(new ServicesTypesRegistration())
                .RegisterDependency(new ApiServicesBootstrapper());

            return container;
        }

        private static IDependencyContainerRegister RegisterDependency(this IDependencyContainerRegister container, IBootstrapper bootstrapper)
            => bootstrapper.RegisterType(container);
    }
}
