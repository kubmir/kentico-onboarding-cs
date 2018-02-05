using System;
using System.Web.Http.Routing;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Services.Services
{
    public class UrlLocationHelper : IUrlLocationHelper
    {
        private readonly UrlHelper _urlHelper;

        public UrlLocationHelper(UrlHelper helper)
        {
            _urlHelper = helper;
        }

        public String GetUrlWithId(String routeName, Guid id)
            => _urlHelper.Route(routeName, id);
    }
}