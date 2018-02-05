using System.Net.Http;
using System.Web;
using Notes.Api.Services.Services;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;

namespace Notes.Api.Services.Dependency
{
    public class ApiServicesBootstrapper : IBootstrapper
    {
        public IDependencyContainer RegisterType(IDependencyContainer container)
            => container
                .RegisterHttpRequestMessage(GetHttpRequestMessage)
                .RegisterType<IUrlLocationHelper, UrlLocationHelper>();

        private static HttpRequestMessage GetHttpRequestMessage()
            => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}