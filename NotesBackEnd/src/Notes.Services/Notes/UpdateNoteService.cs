using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.Notes
{
    public class UpdateNoteService : IUpdateNoteService
    {
        private readonly IDateService _dateService;
        private readonly INotesRepository _repository;

        public UpdateNoteService(IDateService dateService, INotesRepository repository)
        {
            _dateService = dateService;
            _repository = repository;
        }

        public async Task<Note> UpdateNoteAsync(Guid updateNoteId, Note note)
        {
            var updateTime = _dateService.GetCurrentDateTime();
            Note noteToUpdate = new Note { Id = updateNoteId, Text = note.Text, CreationDate = note.CreationDate, LastModificationDate = updateTime };

            var updatedNote = await _repository.UpdateNoteAsync(updateNoteId, noteToUpdate);

            return updatedNote;
        }
    }
}
