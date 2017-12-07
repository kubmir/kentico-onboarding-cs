using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Models.Model;

namespace Notes.Models.Repository
{
    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();

        Task<Note> GetNoteByIdAsync(Guid id);

        Task<Note> CreateNoteAsync(Note note);

        Task<Note> DeleteNoteByIdAsync(Guid id);

        Task<Note> UpdateNote(Note note);
    }
}
