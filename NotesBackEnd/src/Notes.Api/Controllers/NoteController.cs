using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Notes.Api.Model;

namespace Notes.Api.Controllers
{
    public class NoteController : ApiController
    {
        private Note[] _notesList = NotesArray.GetInitialNotes();

        [HttpGet]
        public async Task<IHttpActionResult> FindNotesAsync()
        {
            var items = await Task.FromResult(_notesList);
            return Ok(items);
        }

        [HttpGet]
        public async Task<IHttpActionResult> FindNoteByIdAsync(Guid id)
        {
            var foundItem = await Task.FromResult(_notesList[0]);
            return Ok(foundItem);
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddNoteAsync(string text)
        {
            var newNotesArray = new Note[_notesList.Length + 1];
            _notesList.CopyTo(newNotesArray, 0);
            newNotesArray[_notesList.Length] = new Note(text, new Guid("d0a0f5bc-0f46-43f0-ab4b-06860c3ea19c"), false);
            _notesList = newNotesArray;

            //just for using await in async function - Ok() is also possible
            return await Task.FromResult(Ok(_notesList));
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateNoteAsync(Guid id, string text)
        {
            var noteToUpdate = _notesList.SingleOrDefault(note => note.Id == id);
            if (noteToUpdate != null)
            {
                var index = Array.IndexOf(_notesList, noteToUpdate);
                noteToUpdate.Text = text;
                _notesList[index] = noteToUpdate;
            }

            return await Task.FromResult(Ok(_notesList));
        }

        [HttpDelete]
        public IHttpActionResult DeleteNoteById(int id)
        {
            return null;
        }
    }
}
