using System;
using System.Web.Configuration;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Services.Database
{
    internal class ConnectionOptions : IConnectionOptions
    {
        private const String ConnectionStringName = "MongoDb_Notes_Connection";
        private readonly String _notesDatabaseConnectionString;

        public ConnectionOptions()
            => _notesDatabaseConnectionString = WebConfigurationManager
                                                    .ConnectionStrings[ConnectionStringName]
                                                    .ConnectionString;

        public String GetNotesDatabaseConnectionString()
            => _notesDatabaseConnectionString;
    }
}