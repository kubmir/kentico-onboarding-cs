using System.Web.Http;
using Notes.Api.Services.Helpers;
using Notes.Contracts.Dependency;
using Notes.Dependency;

namespace Notes.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            IMyContainer container = new MyContainer()
                .RegisterDependency(new Repository.Dependency.RepositoryTypesRegistration())
                .RegisterDependency(new Dependency.ApiTypesRegistration());

            config.DependencyResolver = new ContainerResolver(container);
        }

        private static IMyContainer RegisterDependency(this IMyContainer container, IBootstrapper registrationClass)
            => registrationClass.RegisterType(container);
    }
}