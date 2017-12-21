using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Model;
using Notes.Contracts.Services.Notes;

namespace Notes.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/notes/{id?}", Name = NotesRouteName)]
    public class NotesController : ApiController
    {
        internal const string NotesRouteName = "Notes";
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

            return Ok(foundNote);
        }

        public async Task<IHttpActionResult> PostAsync(Note noteToAdd)
        {
            Note addedNote = await _notesServices.CreateNoteAsync(noteToAdd);

            return Created(_locationHelper.GetUrlWithId(NotesRouteName, addedNote.Id), addedNote);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Note noteToUpdate)
        {
            var updatedNote = await _notesServices.UpdateNoteAsync(noteToUpdate);

            return Ok(updatedNote);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            var deletedNote = await _notesServices.DeleteNoteAsync(id);

            return Ok(deletedNote);
        }
    }
}
