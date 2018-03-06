using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using Notes.Api.Routes;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;

namespace Notes.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route(RouteManager.NotesRoute, Name = RouteManager.NotesRouteName)]
    public class NotesController : ApiController
    {
        private readonly INotesRepository _repository;
        private readonly IUrlLocationHelper _locationHelper;

        public NotesController(INotesRepository repository, IUrlLocationHelper locationHelper)
        {
            _repository = repository;
            _locationHelper = locationHelper;
        }

        public async Task<IHttpActionResult> GetAsync()
        {
            var notes = await _repository.GetAllNotesAsync();

            return Ok(notes);
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var foundNote = await _repository.GetNoteByIdAsync(id);

            return Ok(foundNote);
        }

        public async Task<IHttpActionResult> PostAsync(Note noteToAdd)
        {
            Note addedNote = await _repository.CreateNoteAsync(noteToAdd);

            return Created(_locationHelper.GetNotesUrlWithId(addedNote.Id), addedNote);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Note noteToUpdate)
        {
            var updatedNote = await _repository.UpdateNoteAsync(noteToUpdate);

            return Ok(updatedNote);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            var deletedNote = await _repository.DeleteNoteByIdAsync(id);

            return Ok(deletedNote);
        }
    }
}
