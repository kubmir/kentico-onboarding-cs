﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Notes.Api.Model;

namespace Notes.Api.Controllers
{
    public class DummyController : ApiController
    {
        private Note[] _notesList = NotesArray.GetInitialNotes();

        [HttpGet]
        public async Task<IHttpActionResult> FindNotesAsync()
        {
            var items = await Task.FromResult(_notesList);
            return Ok(items);
        }

        [HttpGet]
        public async Task<IHttpActionResult> FindNoteByIdAsync(string id)
        {
            var foundItem = await Task.FromResult(_notesList[0]);
            return Ok(foundItem);
        }

        [HttpPost]
        public IHttpActionResult AddNote(string text)
        {
            return null;
        }

        [HttpPut]
        public IHttpActionResult UpdateNote(string text, bool isEditActive)
        {
            return null;
        }

        [HttpDelete]
        public IHttpActionResult DeleteNoteById(int id)
        {
            return null;
        }
    }
}
