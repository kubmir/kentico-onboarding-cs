using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;

namespace Notes.Services.Notes
{
    public class GetNoteService : IGetNoteService
    {
        private readonly INotesRepository _repository;
        private Note _cachedNote;

        public GetNoteService(INotesRepository repository)
        {
            _cachedNote = null;
            _repository = repository;
        }

        public async Task<Note> GetNoteByIdAsync(Guid id)
        {
            if (_cachedNote?.Id != id)
            {
                _cachedNote = await _repository.GetNoteByIdAsync(id);
            }

            return _cachedNote;
        }

        public async Task<Boolean> IsNoteExistingAsync(Guid id)
            => await GetNoteByIdAsync(id) != null;
    }
}
