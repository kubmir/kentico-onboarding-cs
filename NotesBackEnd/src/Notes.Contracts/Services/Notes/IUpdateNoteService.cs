using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;

namespace Notes.Contracts.Services.Notes
{
    public interface IUpdateNoteService
    {
        Task<Note> UpdateNoteAsync(Guid id, Note note);
    }
}
