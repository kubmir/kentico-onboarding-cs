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

        public async Task<Note> GetByIdAsync(Guid id)
        {
            if (_cachedNote?.Id != id)
            {
                _cachedNote = await _repository.GetByIdAsync(id);
            }

            return _cachedNote;
        }

        public async Task<Boolean> Exists(Guid id)
            => await GetByIdAsync(id) != null;
    }
}
