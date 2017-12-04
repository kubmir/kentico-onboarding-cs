using System.Net;
using Notes.Api.Controllers;
using Notes.Api.Model;
using NUnit.Framework;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Notes.Api.Tests.Utils;

namespace Notes.Api.Tests.Controllers
{
    [TestFixture]
    public class DummyControllerTest
    {
        private DummyController _controller;
        private NoteEqualityComparer _noteEqualityComparer;

        [SetUp]
        public void Init()
        {
            _controller = new DummyController();
            _controller.Configuration = new HttpConfiguration();
            _controller.Request = new HttpRequestMessage();
            _noteEqualityComparer = new NoteEqualityComparer();
        }

        [Test]
        public async Task Get_FindNotesAsync()
        {
            var expectedNotes = new[]
            {
                new Note("First note", "2c00d1c2-fd2b-4c06-8f2d-130e88f719c2", false),
                new Note("Second note", "ebcb3d81-af4e-428f-a22d-e7852d70d3a0", false),
                new Note("Third note", "599442c0-ae28-4157-9a3f-0491bb4ba6c1", false),
                new Note("Fourth note", "4785546e-824d-42a4-900b-e656f19ffb59", false)
            };

            IHttpActionResult response = await _controller.FindNotesAsync();

            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out Note[] actualNotes);

            Assert.Multiple(() =>
            {
                Assert.That(expectedNotes.Length, Is.EqualTo(actualNotes.Length));
                Assert.That(HttpStatusCode.OK, Is.EqualTo(executedResponse.StatusCode));
                Assert.That(expectedNotes, Is.EquivalentTo(actualNotes).Using(_noteEqualityComparer));
            });
        }

        [Test]
        public async Task Get_FindNoteByIdAsync()
        {
            var noteId = "2c00d1c2-fd2b-4c06-8f2d-130e88f719c2";
            var expectedNote = new Note("First note", noteId, false);

            IHttpActionResult response = await _controller.FindNoteByIdAsync(noteId);
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out Note actualNote);

            Assert.Multiple(() =>
            {
                Assert.That(actualNote, Is.EqualTo(expectedNote).Using(_noteEqualityComparer));
                Assert.That(HttpStatusCode.OK, Is.EqualTo(executedResponse.StatusCode));
            });
        }
    }
}
