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
    [Route(RouteManager.NotesRoute, Name = RouteManager.NotesRouteName)]
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
            var notes = await _notesRepository.GetAllNotesAsync();

            return Ok(notes);
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            if (!await _getNoteService.IsNoteExistingAsync(id))
            {
                return NotFound();
            }

            var foundNote = await _getNoteService.GetNoteByIdAsync(id);

            return Ok(foundNote);
        }

        public async Task<IHttpActionResult> PostAsync(Note noteToAdd)
        {
            IsNoteValidForCreation(noteToAdd);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Note addedNote = await _addNoteService.CreateNoteAsync(noteToAdd);

            return Created(_locationHelper.GetNotesUrlWithId(addedNote.Id), addedNote);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Note noteToUpdate)
        {
            IsNoteValidForUpdate(noteToUpdate);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _getNoteService.IsNoteExistingAsync(id))
            {
                var addedNote = await _addNoteService.CreateNoteAsync(noteToUpdate);

                return Created(_locationHelper.GetNotesUrlWithId(addedNote.Id), addedNote);
            }

            var updatedNote = await _updateNoteService.UpdateNoteAsync(id, noteToUpdate);

            return Ok(updatedNote);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            if (!await _getNoteService.IsNoteExistingAsync(id))
            {
                return NotFound();
            }

            var deletedNote = await _notesRepository.DeleteNoteByIdAsync(id);

            return Ok(deletedNote);
        }

        private void IsNoteValidForCreation(Note note)
        {
            if (note == null)
            {
                NoteIsNotValid(nameof(note),
                    "Note cannot be null!");
                return;
            }

            IsNoteTextValid(note);

            if (note.Id != Guid.Empty)
            {
                NoteIsNotValid(nameof(note), 
                    "Note id cannot be specified at this point!");
            }

            if (note.CreationDate != default(DateTime))
            {
               NoteIsNotValid(nameof(note), 
                    "Creation date of note cannot be specified at this point!");
            }

            if (note.LastModificationDate != default(DateTime))
            {
                NoteIsNotValid(nameof(note),
                    "Last modification date of note cannot be specified at this point!");

            }
        }

        private void IsNoteValidForUpdate(Note note)
        {
            if (note == null)
            {
                NoteIsNotValid(nameof(note),
                    "Note cannot be null!");
                return;
            }

            IsNoteTextValid(note);
        }

        private void IsNoteTextValid(Note note)
        {
            if (note.Text.Trim() == String.Empty)
            {
                NoteIsNotValid(nameof(note),
                    "Text of note cannot be empty and cannot consist just of white spaces!");
            }
        }

        private void NoteIsNotValid(string noteName, string message)
            => ModelState.AddModelError(noteName, message);
    }
}
