using System.Collections.Generic;
using System.Web.Http;
using Notes.Api.Model;

namespace Notes.Api.Controllers
{
    public class DummyController : ApiController
    {
        private List<Note> _notesList = NotesList.GetInitialNotes();
    }
}
