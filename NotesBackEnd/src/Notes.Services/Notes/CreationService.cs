using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Wrappers;

namespace Notes.Services.Notes
{
    internal class CreationService : ICreationService
    {
        private readonly IDateWrapper _dateWrapper;
        private readonly INotesRepository _repository;
        private readonly IGuidWrapper _guidWrapper;

        public CreationService(IDateWrapper dateWrapper, INotesRepository repository, IGuidWrapper guidWrapper)
        {
            _dateWrapper = dateWrapper;
            _repository = repository;
            _guidWrapper = guidWrapper;
        }

        public async Task<Note> CreateAsync(Note note)
        {
            var dateTime = _dateWrapper.GetCurrentDateTime();

            var noteToPersist = new Note
            {
                Id = _guidWrapper.GetNew(),
                Text = note.Text,
                CreationDate = dateTime,
                LastModificationDate = dateTime
            };

            return await _repository.CreateAsync(noteToPersist);
        }
    }
}