using Notes.Api.Services.Dependency;
using Notes.Contracts.Dependency;
using Notes.Dependency.Containers;
using Notes.Repository.Dependency;

namespace Notes.Dependency.ContainersBuilders
{
    public static class DependencyContainerBuilder
    {
        public static IDependencyContainer SetUpContainer() =>
            new DependencyContainer()
                .RegisterDependency(new RepositoryTypesBootstrapper())
                .RegisterDependency(new ApiServicesBootstrapper());

        private static IDependencyContainer RegisterDependency(this IDependencyContainer container, IBootstrapper bootstrapper)
            => bootstrapper.RegisterType(container);
    }
}
