using Notes.Api.Helpers;
using Notes.Contracts.ApiHelpers;
using Notes.Contracts.Dependency;
using Unity;

namespace Notes.Api.Dependency
{
    public class ApiTypesRegistration : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container)
            => container.RegisterType<IUrlLocationHelper, UrlLocationHelper>();
    }
}