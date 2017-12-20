﻿using System;
using System.Net.Http;
using System.Web.Http.Routing;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Services.Services
{
    internal class UrlLocationHelper : IUrlLocationHelper
    {
        private readonly HttpRequestMessage _httpRequestMessage;

        public UrlLocationHelper(HttpRequestMessage requestMessage)
        {
            _httpRequestMessage = requestMessage;
        }

        public String GetUrlWithId(String routeName, Guid id)
        {
            return new UrlHelper(_httpRequestMessage).Route(routeName,  new { id });
        }
    }
}