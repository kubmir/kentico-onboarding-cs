using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;

namespace Notes.Contracts.Services.Notes
{
    public interface IGetNoteService
    {
        Task<Note> GetByIdAsync(Guid id);

        Task<Boolean> Exists(Guid id);
    }
}
