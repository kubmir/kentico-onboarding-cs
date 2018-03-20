using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Wrappers;

namespace Notes.Services.Notes
{
    internal class UpdateService : IUpdateService
    {
        private readonly IDateWrapper _dateWrapper;
        private readonly INotesRepository _repository;
        private readonly IRetrievalService _retrievalService;

        public UpdateService(IDateWrapper dateWrapper, INotesRepository repository, IRetrievalService retrievalService)
        {
            _dateWrapper = dateWrapper;
            _repository = repository;
            _retrievalService = retrievalService;
        }

        public async Task<Note> UpdateAsync(Guid updateNoteId, Note note)
        {
            var persistedNote = await _retrievalService.GetByIdAsync(updateNoteId);

            var noteToUpdate = new Note
            {
                Id = updateNoteId,
                Text = note.Text,
                CreationDate = persistedNote.CreationDate,
                LastModificationDate = _dateWrapper.GetCurrentDateTime()
            };

            return await _repository.UpdateAsync(noteToUpdate);
        }
    }
}
