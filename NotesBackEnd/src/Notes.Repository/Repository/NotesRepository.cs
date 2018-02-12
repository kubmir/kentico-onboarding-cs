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

        public NotesRepository(IDatabaseContext databaseContext)
        {
            _persistedNotes = databaseContext.GetPersistedNotes();
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

        public async Task<Note> UpdateNoteAsync(Note note)
        {
            var filter = Builders<Note>.Filter.Eq("Id", note.Id);
            var update = Builders<Note>
                .Update
                .Set("Text", note.Text)
                .CurrentDate("LastModificationDate");

            return await _persistedNotes.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<Note> DeleteNoteByIdAsync(Guid id)
            => await _persistedNotes.FindOneAndDeleteAsync(note => note.Id == id);
    }
}
