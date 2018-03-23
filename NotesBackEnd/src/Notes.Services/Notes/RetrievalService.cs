using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;

namespace Notes.Services.Notes
{
    internal class RetrievalService : IRetrievalService
    {
        private readonly INotesRepository _repository;
        private Note _cachedNote;

        public RetrievalService(INotesRepository repository)
            => _repository = repository;

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