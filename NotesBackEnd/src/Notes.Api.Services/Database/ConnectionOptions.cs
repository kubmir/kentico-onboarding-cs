using System.Web.Configuration;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Services.Database
{
    internal class ConnectionOptions : IConnectionOptions
    {
        private readonly string _mongoDatabaseConnectionString;

        public ConnectionOptions()
            => _mongoDatabaseConnectionString = WebConfigurationManager.ConnectionStrings["MongoDb_Notes_Connection"].ConnectionString;

        public string GetDatabaseConnectionString()
            => _mongoDatabaseConnectionString;
    }
}