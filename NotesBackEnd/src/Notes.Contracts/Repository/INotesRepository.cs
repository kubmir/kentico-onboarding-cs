using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Contracts.Model;

namespace Notes.Contracts.Repository
{
    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetAllAsync();

        Task<Note> GetByIdAsync(Guid id);

        Task<Note> CreateAsync(Note note);

        Task<Note> DeleteByIdAsync(Guid id);

        Task<Note> UpdateAsync(Note noteToUpdate);
    }
}
