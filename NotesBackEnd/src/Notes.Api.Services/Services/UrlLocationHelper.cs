using System;
using System.Web.Http.Routing;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Services.Services
{
    internal class UrlLocationHelper : IUrlLocationHelper
    {
        private readonly UrlHelper _urlHelper;
        private readonly IRouteManager _routeManager;

        public UrlLocationHelper(UrlHelper helper, IRouteManager routeManager)
        {
            _urlHelper = helper;
            _routeManager = routeManager;
        }

        public String GetNotesUrlWithId(Guid id)
            => _urlHelper.Route(_routeManager.GetNotesRouteName(), new { id });
    }
}