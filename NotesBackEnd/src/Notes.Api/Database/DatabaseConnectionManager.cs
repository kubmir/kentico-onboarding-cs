using System.Web.Configuration;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Database
{
    internal class DatabaseConnectionManager : IConnectionStringManager
    {
        private readonly string _mongoDatabaseConnectionString;

        public DatabaseConnectionManager()
            => _mongoDatabaseConnectionString = WebConfigurationManager.ConnectionStrings["MongoDb_Notes_Connection"].ConnectionString;

        public string GetDatabaseConnectionString()
            => _mongoDatabaseConnectionString;
    }
}