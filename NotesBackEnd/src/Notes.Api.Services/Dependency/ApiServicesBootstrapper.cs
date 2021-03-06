﻿using System.Net.Http;
using System.Web;
using System.Web.Http.Dependencies;
using Notes.Api.Services.Database;
using Notes.Api.Services.Services;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;

namespace Notes.Api.Services.Dependency
{
    public class ApiServicesBootstrapper : IBootstrapper
    {
        public IContainer RegisterType(IContainer container)
            => container
                .RegisterType(GetHttpRequestMessage, LifetimeTypes.PerRequestSingleton)
                .RegisterType<IDependencyResolver, ContainerResolver>(LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IConnectionOptions, ConnectionOptions>(LifetimeTypes.PerApplicationSingleton)
                .RegisterType<IUrlLocationHelper, UrlLocationHelper>(LifetimeTypes.PerRequestSingleton);

        private static HttpRequestMessage GetHttpRequestMessage()
            => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];
    }
}