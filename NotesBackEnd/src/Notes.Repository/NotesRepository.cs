using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Models.Model;
using Notes.Models.Repository;

namespace Notes.Repository
{
    public class NotesRepository : INotesRepository
    {
        public Task<Note> CreateNoteAsync(Note note)
        {
            throw new NotImplementedException();
        }

        public Task<Note> DeleteNoteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetNoteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Note> UpdateNote(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
