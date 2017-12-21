using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Contracts.Model;

namespace Notes.Contracts.Services.Notes
{
    public interface INotesServices
    {
        Task<Note> CreateNoteAsync(Note note);

        Task<Note> GetNoteAsync(Guid id);

        Task<IEnumerable<Note>> GetAllNotesAsync();

        Task<Note> UpdateNoteAsync(Note note);

        Task<Note> DeleteNoteAsync(Guid id);
    }
}
