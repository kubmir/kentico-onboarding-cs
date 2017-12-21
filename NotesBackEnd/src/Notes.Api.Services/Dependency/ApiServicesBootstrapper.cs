﻿using System.Net.Http;
using Notes.Api.Services.Services;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;

namespace Notes.Api.Services.Dependency
{
    public class ApiServicesBootstrapper : IBootstrapper
    {
        public IDependencyContainer RegisterType(IDependencyContainer container)
            => container
                .RegisterType<IUrlLocationHelper, UrlLocationHelper>()
                .RegisterHttpRequestMessage<HttpRequestMessage>();
    }
}