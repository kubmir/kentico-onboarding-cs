﻿using System.Net.Http;
using Notes.Api.Services.Database;
using Notes.Api.Services.Services;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;

namespace Notes.Api.Services.Dependency
{
    public class ApiServicesTypesRegistration : IBootstrapper
    {
        public IDependencyContainer RegisterType(IDependencyContainer container)
            => container
                .RegisterType<IUrlLocationHelper, UrlLocationHelper>()
                .RegisterType<HttpRequestMessage>()
                .RegisterType<IDatabaseConnectionLoader, DatabaseConnectionLoader>();
    }
}