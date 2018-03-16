using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;

namespace Notes.Contracts.Services.Notes
{
    public interface IUpdateService
    {
        Task<Note> UpdateAsync(Guid id, Note note);
    }
}
