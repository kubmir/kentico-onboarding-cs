using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.Notes
{
    internal class AddNoteService : IAddNoteService
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

        public async Task<Note> CreateAsync(Note note)
        {
            var dateTime = _dateService.GetCurrentDateTime();

            var noteToPersist = new Note()
            {
                Id = _guidService.GetNew(),
                Text = note.Text,
                CreationDate = dateTime,
                LastModificationDate = dateTime
            };

            return await _repository.CreateAsync(noteToPersist);
        }
    }
}
