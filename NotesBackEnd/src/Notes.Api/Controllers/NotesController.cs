using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using Notes.Api.Services.Services;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Model;
using Notes.Contracts.Services.Notes;

namespace Notes.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route(RouteManager.NotesRoute, Name = RouteManager.NotesRouteName)]
    public class NotesController : ApiController
    {
        private readonly IUrlLocationHelper _locationHelper;
        private readonly INotesServices _notesServices;

        public NotesController(IUrlLocationHelper locationHelper, INotesServices notesServices)
        {
            _locationHelper = locationHelper;
            _notesServices = notesServices;
        }

        public async Task<IHttpActionResult> GetAsync()
        {
            var notes = await _notesServices.GetAllNotesAsync();

            return Ok(notes);
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var foundNote = await _notesServices.GetNoteAsync(id);

            if (foundNote == null)
            {
                return NotFound();
            }

            return Ok(foundNote);
        }

        public async Task<IHttpActionResult> PostAsync(Note noteToAdd)
        {
            if (!IsNoteValidForCreation(noteToAdd))
            {
                return BadRequest(ModelState);
            }

            Note addedNote = await _notesServices.CreateNoteAsync(noteToAdd);

            return Created(_locationHelper.GetNotesUrlWithId(addedNote.Id), addedNote);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Note noteToUpdate)
        {
            if (!IsNoteValidForUpdate(noteToUpdate))
            {
                return BadRequest(ModelState);
            }

            var updatedNote = await _notesServices.UpdateNoteAsync(id, noteToUpdate);

            if (updatedNote == null)
            {
                return NotFound();
            }

            return Ok(updatedNote);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            var deletedNote = await _notesServices.DeleteNoteAsync(id);

            if (deletedNote == null)
            {
                return NotFound();
            }

            return Ok(deletedNote);
        }

        private bool IsNoteValidForCreation(Note note)
        {
            if (!IsNoteNotNullWithValidText(note))
            {
                return false;
            }

            if (note.Id != Guid.Empty)
            {
                return NoteIsNotValid(nameof(note), 
                    "Note id cannot be specified at this point!");
            }

            if (note.CreationDate != default(DateTime))
            {
                return NoteIsNotValid(nameof(note), 
                    "Creation date of note cannot be specified at this point!");
            }

            if (note.LastModificationDate != default(DateTime))
            {
                return NoteIsNotValid(nameof(note),
                    "Last modification date of note cannot be specified at this point!");

            }

            return true;
        }

        private bool IsNoteValidForUpdate(Note note)
            => IsNoteNotNullWithValidText(note);

        private bool IsNoteNotNullWithValidText(Note note)
        {
            if (note == null)
            {
                return NoteIsNotValid(nameof(note),
                    "Note cannot be null!");
            }

            if (note.Text.Trim() == String.Empty)
            {
                return NoteIsNotValid(nameof(note),
                    "Text of note cannot be empty and cannot consist just of white spaces!");
            }

            return true;
        }

        private bool NoteIsNotValid(string noteName, string message)
        {
            ModelState.AddModelError(noteName, message);
            return false;
        }
    }
}
