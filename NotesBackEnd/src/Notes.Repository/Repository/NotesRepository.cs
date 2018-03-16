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

        public NotesRepository(IConnectionOptions connectionOptions)
        {
            var connectionString = connectionOptions.GetNotesDatabaseConnectionString();
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);

            _persistedNotes = database.GetCollection<Note>(NoteCollectionName);
        }

        public async Task<IEnumerable<Note>> GetAllAsync()
        {
            var allLoadedNotes = await _persistedNotes.FindAsync<Note>(FilterDefinition<Note>.Empty);

            return allLoadedNotes.ToEnumerable();
        }

        public async Task<Note> GetByIdAsync(Guid id)
        {
            var loadedNote = await _persistedNotes.FindAsync(persistedNote => persistedNote.Id == id);

            return loadedNote.FirstOrDefault();
        }

        public async Task<Note> CreateAsync(Note note)
        {
            await _persistedNotes.InsertOneAsync(note);

            return note;
        }

        public async Task<Note> UpdateAsync(Guid id, Note noteToUpdate)
            => await _persistedNotes.FindOneAndReplaceAsync(databaseNote => databaseNote.Id == id, noteToUpdate);      

        public async Task<Note> DeleteByIdAsync(Guid id)
            => await _persistedNotes.FindOneAndDeleteAsync(note => note.Id == id);
    }
}
