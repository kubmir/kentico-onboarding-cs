using System;
using System.Net;
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
        private readonly ICreationService _creationService;
        private readonly IUpdateService _updateService;
        private readonly INotesRepository _notesRepository;
        private readonly IRetrievalService _retrievalService;

        public NotesController(IUrlLocationHelper locationHelper, ICreationService creationService, IUpdateService updateService, INotesRepository notesRepository, IRetrievalService retrievalService)
        {
            _locationHelper = locationHelper;
            _creationService = creationService;
            _updateService = updateService;
            _notesRepository = notesRepository;
            _retrievalService = retrievalService;
        }

        public async Task<IHttpActionResult> GetAsync()
        {
            var notes = await _notesRepository.GetAllAsync();

            return Ok(notes);
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            ModelStateIdValidation(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _retrievalService.Exists(id))
            {
                return NotFound();
            }

            var foundNote = await _retrievalService.GetByIdAsync(id);

            return Ok(foundNote);
        }

        public async Task<IHttpActionResult> PostAsync(Note noteToAdd)
        {
            ModelStateNoteForCreationValidation(noteToAdd);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedNote = await _creationService.CreateAsync(noteToAdd);

            return Created(_locationHelper.GetNotesUrlWithId(addedNote.Id), addedNote);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, Note noteToUpdate)
        {
            ModelStateNoteForUpdateValidation(noteToUpdate);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noteToUpdate.Id)
            {
                return Content(
                    HttpStatusCode.Conflict,
                    $"Identifier provided in URI ({id}) does not match the identifier provided in the body ({noteToUpdate.Id})");
            }

            if (!await _retrievalService.Exists(id))
            {
                if (id == Guid.Empty)
                {
                    return await PostAsync(noteToUpdate);
                }

                return NotFound();
            }

            var updatedNote = await _updateService.UpdateAsync(id, noteToUpdate);

            return Ok(updatedNote);
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            if (!await _retrievalService.Exists(id))
            {
                return NotFound();
            }

            var deletedNote = await _notesRepository.DeleteByIdAsync(id);

            return Ok(deletedNote);
        }

        private void ModelStateNoteForCreationValidation(Note note)
        {
            if (note == null)
            {
                AddValidationError(nameof(note), "Note cannot be null!");
                return;
            }

            ModelStateTextValidation(note);

            if (note.Id != Guid.Empty)
            {
                AddValidationError(nameof(note.Id), "Note id cannot be specified at this point!");
            }

            if (note.CreationDate != default(DateTime))
            {
               AddValidationError(nameof(note.CreationDate), "Creation date of note cannot be specified at this point!");
            }

            if (note.LastModificationDate != default(DateTime))
            {
                AddValidationError(nameof(note.LastModificationDate), "Last modification date of note cannot be specified at this point!");

            }
        }

        private void ModelStateNoteForUpdateValidation(Note note)
        {
            if (note == null)
            {
                AddValidationError(nameof(note), "Note cannot be null!");
                return;
            }

            ModelStateTextValidation(note);
        }

        private void ModelStateTextValidation(Note note)
        {
            if (String.IsNullOrWhiteSpace(note.Text))
            {
                AddValidationError(nameof(note.Text), "Text of note cannot be empty and cannot consist just of white spaces!");
            }
        }

        private void ModelStateIdValidation(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(nameof(id), "Id of requested note cannot be empty!");
            }
        }

        private void AddValidationError(String errorField, String message)
            => ModelState.AddModelError(errorField, message);
    }
}
