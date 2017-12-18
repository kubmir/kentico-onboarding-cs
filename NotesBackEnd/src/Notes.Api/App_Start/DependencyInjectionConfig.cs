using System.Web.Http;
using Notes.Api.Helpers;
using Notes.Contracts.Dependency;
using Unity;

namespace Notes.Api
{
    internal static class DependencyInjectionConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            IUnityContainer container = new UnityContainer()
                .RegisterDependency(new Repository.Dependency.RepositoryTypesRegistration())
                .RegisterDependency(new Dependency.ApiTypesRegistration());

            config.DependencyResolver = new UnityResolver(container);
        }

        private static IUnityContainer RegisterDependency(this IUnityContainer container, IBootstrapper registrationClass)
            => registrationClass.RegisterType(container);
    }
}