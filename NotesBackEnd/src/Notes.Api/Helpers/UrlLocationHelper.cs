using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using Notes.Contracts.ApiHelpers;

namespace Notes.Api.Helpers
{
    internal class UrlLocationHelper : IUrlLocationHelper
    {
        private readonly HttpRequestMessage _httpRequestMessage;

        public UrlLocationHelper()
        {
            _httpRequestMessage = (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
        }

        public String GetUrl(Guid id)
        {
            return new UrlHelper(_httpRequestMessage).Route("Notes",  new { id });
        }
    }
}