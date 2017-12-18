using System.Net.Http;
using System.Web;
using Notes.Api.Helpers;
using Notes.Contracts.ApiHelpers;
using Notes.Contracts.Dependency;
using Unity;
using Unity.Injection;

namespace Notes.Api.Dependency
{
    public class ApiTypesRegistration : IBootstrapper
    {
        public IUnityContainer RegisterType(IUnityContainer container)
            => container
                .RegisterType<IUrlLocationHelper, UrlLocationHelper>()
                .RegisterType<HttpRequestMessage>(new InjectionFactory(GetHttpRequestMessage));

        private HttpRequestMessage GetHttpRequestMessage(IUnityContainer container) 
            => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];

    }
}