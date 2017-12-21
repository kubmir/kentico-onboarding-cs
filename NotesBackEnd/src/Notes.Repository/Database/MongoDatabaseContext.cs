using MongoDB.Driver;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Model;

namespace Notes.Repository.Database
{
    internal class MongoDatabaseContext : IDatabaseContext
    {
        private readonly string _connectionString;

        public MongoDatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IMongoCollection<Note> GetPersistedNotes()
        {
            var mongoClient = new MongoClient(_connectionString);
            var databaseName = new MongoUrl(_connectionString).DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);

            return database.GetCollection<Note>("notes");
        }
    }
}
