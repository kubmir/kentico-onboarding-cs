using MongoDB.Driver;
using Notes.Contracts.Model;

namespace Notes.Contracts.ApiServices
{
    public interface IDatabaseContext
    {
        IMongoCollection<Note> GetPersistedNotes();
    }
}
