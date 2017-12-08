﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Repository;

namespace Notes.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/notes/{id?}", Name = "Notes")]
    public class NotesController : ApiController
    {
        private INotesRepository _repository = new NotesRepository();

        public async Task<IHttpActionResult> GetAsync()
        {
            var notes = await _repository.GetAllNotesAsync();

            return await Task.FromResult(Ok(notes));
        }

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            var foundNote = await _repository.GetNoteByIdAsync(id);

            return await Task.FromResult(Ok(foundNote));
        }

        public async Task<IHttpActionResult> PostAsync(Note noteToAdd)
        {
            Note addedNote = await _repository.CreateNoteAsync(noteToAdd);

            return await Task.FromResult(CreatedAtRoute("Notes", new { id = addedNote.Id.ToString() }, addedNote));
        }

        public async Task<IHttpActionResult> PutAsync(Note noteToUpdate)
        {
            var updatedNote = await _repository.UpdateNoteAsync(noteToUpdate);

            return await Task.FromResult(Ok(updatedNote));
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            var deletedNote = await _repository.DeleteNoteByIdAsync(id);

            return await Task.FromResult(Ok(deletedNote));
        }
    }
}
