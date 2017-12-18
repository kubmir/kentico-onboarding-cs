using System.Net.Http;
using Notes.Api.Services.Helpers;
using Notes.Contracts.ApiHelpers;
using Notes.Contracts.Dependency;

namespace Notes.Api.Dependency
{
    public class ApiTypesRegistration : IBootstrapper
    {
        public IDependencyContainer RegisterType(IDependencyContainer container)
            => container
                .RegisterType<IUrlLocationHelper, UrlLocationHelper>()
                .RegisterType<HttpRequestMessage>();
    }
}