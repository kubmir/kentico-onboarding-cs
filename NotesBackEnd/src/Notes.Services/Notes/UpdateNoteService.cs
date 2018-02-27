using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Date;
using Notes.Contracts.Services.Notes;

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

        public async Task<Note> UpdateNoteAsync(Note note)
        {
            var updateTime = _dateService.GetCurrentDateTime();
            Note noteToUpdate = new Note { Id = note.Id, Text = note.Text, CreationDate = note.CreationDate, LastModificationDate = updateTime };

            var updatedNote = await _repository.UpdateNoteAsync(note.Id, noteToUpdate);

            return updatedNote;
        }
    }
}
