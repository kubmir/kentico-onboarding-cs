using System.Configuration;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Services.Database
{
    public class DatabaseConnectionLoader : IDatabaseConnectionLoader
    {
        public string GetNotesDatabaseConnectionString()
        {
            var connection = ConfigurationManager.ConnectionStrings["MongoDb_Notes_Connection"];

            return connection.ConnectionString;
        }
    }
}
