using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.Notes
{
    internal class UpdateNoteService : IUpdateNoteService
    {
        private readonly IDateService _dateService;
        private readonly INotesRepository _repository;
        private readonly IGetNoteService _getService;

        public UpdateNoteService(IDateService dateService, INotesRepository repository, IGetNoteService getService)
        {
            _dateService = dateService;
            _repository = repository;
            _getService = getService;
        }

        public async Task<Note> UpdateAsync(Guid updateNoteId, Note note)
        {
            var persistedNote = await _getService.GetByIdAsync(updateNoteId);

            var noteToUpdate = new Note
            {
                Id = updateNoteId,
                Text = note.Text,
                CreationDate = persistedNote.CreationDate,
                LastModificationDate = _dateService.GetCurrentDateTime()
            };

            return await _repository.UpdateAsync(updateNoteId, noteToUpdate);
        }
    }
}
