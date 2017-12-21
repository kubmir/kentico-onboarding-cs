﻿using System.Web.Configuration;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Dependency;
using Notes.Contracts.Repository;
using Notes.Repository.Database;
using Notes.Repository.Repository;

namespace Notes.Repository.Dependency
{
    public class RepositoryTypesBootstrapper : IBootstrapper
    {
        public IDependencyContainer RegisterType(IDependencyContainer container)
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["MongoDb_Notes_Connection"].ConnectionString;

            return container
                .RegisterType<INotesRepository, NotesRepository>()
                .RegisterType<IDatabaseContext, MongoDatabaseContext>(connectionString);
        }
    }
}