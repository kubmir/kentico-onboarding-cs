using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using Notes.Api.Model;

namespace Notes.Api.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/notes")]
    public class NoteController : ApiController
    {
        private static readonly Note[] NotesList = {
            new Note {Text = "First note", Id = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2")},
            new Note {Text = "Second note", Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0")},
            new Note {Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1")},
            new Note {Text = "Fourth note", Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59")}
        };

        [HttpGet]
        [Route("find/notes")]
        public async Task<IHttpActionResult> FindNotesAsync()
        {
            var items = await Task.FromResult(NotesList);
            return Ok(items);
        }

        [HttpGet]
        [Route("find/{id}")]

        public async Task<IHttpActionResult> FindNoteByIdAsync(Guid id)
        {
            var foundItem = await Task.FromResult(NotesList[0]);
            return Ok(foundItem);
        }

        [HttpPost]
        [Route("add/{text}")]
        public async Task<IHttpActionResult> AddNoteAsync(string text)
        {
            return await Task.FromResult(Created($"/find/{NotesList[1].Id}", NotesList[1]));
        }

        [HttpPut]
        [Route("update/{text}")]
        public async Task<IHttpActionResult> UpdateNoteAsync(Guid id, string text)
        {
            return await Task.FromResult(Ok(NotesList[2]));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> DeleteNoteByIdAsync(Guid id)
        {
            return await Task.FromResult(Ok(NotesList[3]));
        }
    }
}
