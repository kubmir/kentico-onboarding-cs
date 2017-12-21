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
        private static readonly Note[] Notes = {
            new Note {Text = "First note", Id = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2")},
            new Note {Text = "Second note", Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0")},
            new Note {Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1")},
            new Note {Text = "Fourth note", Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59")}
        };

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

        public Task<Note> UpdateNoteAsync(Note note)
            => Task.FromResult(Notes[2]);

        public Task<Note> DeleteNoteByIdAsync(Guid id)
            => Task.FromResult(Notes[3]);
    }
}
