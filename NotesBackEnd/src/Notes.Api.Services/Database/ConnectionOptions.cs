using System.Web.Configuration;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Services.Database
{
    internal class ConnectionOptions : IConnectionOptions
    {
        private const string ConnectionStringName = "MongoDb_Notes_Connection";
        private readonly string _notesDatabaseConnectionString;

        public ConnectionOptions()
            => _notesDatabaseConnectionString = WebConfigurationManager
                                                    .ConnectionStrings[ConnectionStringName]
                                                    .ConnectionString;

        public string GetNotesDatabaseConnectionString()
            => _notesDatabaseConnectionString;
    }
}