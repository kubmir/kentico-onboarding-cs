using System;
using System.Web.Http.Routing;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Services.Services
{
    internal class UrlLocationHelper : IUrlLocationHelper
    {
        private readonly UrlHelper _urlHelper;
        private readonly IRouteOptions _routeOptions;

        public UrlLocationHelper(UrlHelper helper, IRouteOptions routeOptions)
        {
            _urlHelper = helper;
            _routeOptions = routeOptions;
        }

        public String GetNotesUrlWithId(Guid id)
            => _urlHelper.Route(_routeOptions.GetNotesRouteName(), new { id });
    }
}