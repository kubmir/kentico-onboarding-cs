using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using Notes.Api.Routes;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;

namespace Notes.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route(RouteOptions.NotesRoute, Name = RouteOptions.NotesRouteName)]
    public class NotesController : ApiController
    {
        private readonly IUrlLocationHelper _locationHelper;
        private readonly IAddNoteService _addNoteService;
        private readonly IUpdateNoteService _updateNoteService;
        private readonly INotesRepository _notesRepository;
        private readonly IGetNoteService _getNoteService;

        public NotesController(IUrlLocationHelper locationHelper, IAddNoteService addService, IUpdateNoteService updateNoteService, INotesRepository notesRepository, IGetNoteService getService)
        {
            _locationHelper = locationHelper;
            _addNoteService = addService;
            _updateNoteService = updateNoteService;
            _notesRepository = notesRepository;
            _getNoteService = getService;
        }

        public async Task<IHttpActionResult> GetAsync()
        {
            var notes = await _notesRepository.GetAllAsync();

            return Ok(notes);
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            IsIdValid(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _getNoteService.Exists(id))
            {
                return NotFound();
            }

            var foundNote = await _getNoteService.GetByIdAsync(id);

            return Ok(foundNote);
        }

        public async Task<IHttpActionResult> PostAsync(Note noteToAdd)
        {
            IsNoteValidForCreation(noteToAdd);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Note addedNote = await _addNoteService.CreateAsync(noteToAdd);

            return Created(_locationHelper.GetNotesUrlWithId(addedNote.Id), addedNote);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Note noteToUpdate)
        {
            IsNoteValidForUpdate(noteToUpdate);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noteToUpdate.Id)
            {
                return Conflict();
            }

            if (!await _getNoteService.Exists(id))
            {
                var addedNote = await _addNoteService.CreateAsync(noteToUpdate);

                return Created(_locationHelper.GetNotesUrlWithId(addedNote.Id), addedNote);
            }

            var updatedNote = await _updateNoteService.UpdateAsync(id, noteToUpdate);

            return Ok(updatedNote);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            if (!await _getNoteService.Exists(id))
            {
                return NotFound();
            }

            var deletedNote = await _notesRepository.DeleteByIdAsync(id);

            return Ok(deletedNote);
        }

        private void IsNoteValidForCreation(Note note)
        {
            if (note == null)
            {
                NoteIsNotValid(nameof(note), "Note cannot be null!");
                return;
            }

            IsNoteTextValid(note);

            if (note.Id != Guid.Empty)
            {
                NoteIsNotValid(nameof(note.Id), "Note id cannot be specified at this point!");
            }

            if (note.CreationDate != default(DateTime))
            {
               NoteIsNotValid(nameof(note.CreationDate), "Creation date of note cannot be specified at this point!");
            }

            if (note.LastModificationDate != default(DateTime))
            {
                NoteIsNotValid(nameof(note.LastModificationDate), "Last modification date of note cannot be specified at this point!");

            }
        }

        private void IsNoteValidForUpdate(Note note)
        {
            if (note == null)
            {
                NoteIsNotValid(nameof(note), "Note cannot be null!");
                return;
            }

            IsNoteTextValid(note);
        }

        private void IsNoteTextValid(Note note)
        {
            if (String.IsNullOrWhiteSpace(note.Text))
            {
                NoteIsNotValid(nameof(note.Text), "Text of note cannot be empty and cannot consist just of white spaces!");
            }
        }

        private void IsIdValid(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(nameof(id), "Id of requested note cannot be empty!");
            }
        }

        private void NoteIsNotValid(string errorField, string message)
            => ModelState.AddModelError(errorField, message);
    }
}
