using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Web.Http;
using Notes.Api.Model;

namespace Notes.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/notes/{id?}", Name = "Notes")]
    public class NoteController : ApiController
    {
        private static readonly Note[] Notes = {
            new Note {Text = "First note", Id = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2")},
            new Note {Text = "Second note", Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0")},
            new Note {Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1")},
            new Note {Text = "Fourth note", Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59")}
        };

        public async Task<IHttpActionResult> GetAsync()
            => await Task.FromResult(Ok(Notes));

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => await Task.FromResult(Ok(Notes[0]));

        public async Task<IHttpActionResult> PostAsync(Note noteToAdd)
            => await Task.FromResult(CreatedAtRoute("Notes", new { id = Notes[1].Id.ToString() }, Notes[1]));

        public async Task<IHttpActionResult> PutAsync(Note noteToUpdate)
            => await Task.FromResult(Ok(Notes[2]));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
            => await Task.FromResult(Ok(Notes[3]));
    }
}
