using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Date;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.Notes
{
    public class AddNoteService : IAddNoteService
    {
        private readonly IDateService _dateService;
        private readonly INotesRepository _repository;
        private readonly IGuidService _guidService;

        public AddNoteService(IDateService dateService, INotesRepository repository, IGuidService guidService)
        {
            _dateService = dateService;
            _repository = repository;
            _guidService = guidService;
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            var dateTime = _dateService.GetCurrentDateTime();

            note.CreationDate = dateTime;
            note.LastModificationDate = dateTime;
            note.Id = _guidService.GetNewGuid();

            return await _repository.CreateNoteAsync(note);
        }
    }
}
