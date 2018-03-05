using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;

namespace Notes.Repository.Repository
{
    internal class NotesRepository : INotesRepository
    {
        private readonly IMongoCollection<Note> _persistedNotes;
        private const string NoteCollectionName = "notes";

        public NotesRepository(IConnectionStringManager connectionManager)
        {
            var connectionString = connectionManager.GetDatabaseConnectionString();
            var mongoClient = new MongoClient(connectionString);
            var databaseName = new MongoUrl(connectionString).DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);

            _persistedNotes = database.GetCollection<Note>(NoteCollectionName);
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
            => (await _persistedNotes.FindAsync<Note>(FilterDefinition<Note>.Empty)).ToEnumerable();

        public async Task<Note> GetNoteByIdAsync(Guid id)
            => (await _persistedNotes.FindAsync(persistedNote => persistedNote.Id == id)).FirstOrDefault();

        public async Task<Note> CreateNoteAsync(Note note)
        {
            await _persistedNotes.InsertOneAsync(note);

            return note;
        }

        public async Task<Note> UpdateNoteAsync(Guid id, Note noteToUpdate)
        {
            var updateDefinition = Builders<Note>
                .Update
                .Set(databaseNote => databaseNote, noteToUpdate);

            return await _persistedNotes.FindOneAndUpdateAsync(databaseNote => databaseNote.Id == id, updateDefinition);
        }

        public async Task<Note> DeleteNoteByIdAsync(Guid id)
            => await _persistedNotes.FindOneAndDeleteAsync(note => note.Id == id);
    }
}
