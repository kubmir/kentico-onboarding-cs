using System.Web.Configuration;
using Notes.Contracts.ApiServices;

namespace Notes.Api.Services.Database
{
    public class DatabaseConnectionLoader : IDatabaseConnectionLoader
    {
        public string GetNotesDatabaseConnectionString()
        {
            var connectionSettings = WebConfigurationManager.ConnectionStrings["MongoDb_Notes_Connection"];
            var connectionString = connectionSettings.ConnectionString;

            return connectionString;
        }
    }
}
