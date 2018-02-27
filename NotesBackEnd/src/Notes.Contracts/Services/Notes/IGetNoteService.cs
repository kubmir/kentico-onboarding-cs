using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;

namespace Notes.Contracts.Services.Notes
{
    public interface IGetNoteService
    {
        Task<Note> GetNoteByIdAsync(Guid id);

        Task<Boolean> IsNoteExistingAsync(Guid id);
    }
}
