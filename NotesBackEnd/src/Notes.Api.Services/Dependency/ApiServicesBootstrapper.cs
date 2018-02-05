using System.Net.Http;
using System.Web;
using Notes.Api.Services.Services;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;

namespace Notes.Api.Services.Dependency
{
    public class ApiServicesBootstrapper : IBootstrapper
    {
        public IDependencyContainerRegister RegisterType(IDependencyContainerRegister container)
            => container
                .RegisterType(GetHttpRequestMessage, LifetimeTypes.PerRequestSingleton)
                .RegisterType<IUrlLocationHelper, UrlLocationHelper>(LifetimeTypes.PerApplicationSingleton);

        private static HttpRequestMessage GetHttpRequestMessage()
            => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}