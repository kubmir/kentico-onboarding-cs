using System.Net.Http;
using System.Web;
using Notes.Api.Helpers;
using Notes.Contracts.ApiHelpers;
using Notes.Contracts.Dependency;

namespace Notes.Api.Dependency
{
    public class ApiTypesRegistration : IBootstrapper
    {
        public IMyContainer RegisterType(IMyContainer container)
            => container
                .RegisterType<IUrlLocationHelper, UrlLocationHelper>()
                .RegisterType<HttpRequestMessage>();
    }
}