using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Date;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Utils;

namespace Notes.Services.Notes
{
    internal class NotesServices : INotesServices
    {
        private readonly IDateService _dateService;
        private readonly INotesRepository _repository;
        private readonly IGuidService _guidService;

        public NotesServices(IDateService dateService, INotesRepository repository, IGuidService guidService)
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

        public async Task<Note> GetNoteAsync(Guid id)
            => await _repository.GetNoteByIdAsync(id);

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
            => await _repository.GetAllNotesAsync();
        

        public async Task<Note> UpdateNoteAsync(Guid id, Note note)
            => await _repository.UpdateNoteAsync(id, note);
        

        public async Task<Note> DeleteNoteAsync(Guid id)
            => await _repository.DeleteNoteByIdAsync(id);
    }
}
