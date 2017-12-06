using System;
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
    public class NoteControllerTest
    {
        private NoteController _controller;

        [SetUp]
        public void Init()
        {
            _controller = new NoteController();
            _controller.Configuration = new HttpConfiguration();
            _controller.Configuration.Routes.MapHttpRoute(
                name: "Notes",
                routeTemplate: "{id}/test");

            _controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://test")
            };
        }

        [Test]
        public async Task GetAsync_FindAllNotes()
        {
            var expectedNotes = new[]
            {
                new Note{ Text = "First note", Id = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2")},
                new Note{ Text = "Second note", Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0")},
                new Note{ Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1")},
                new Note{ Text = "Fourth note", Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59")}
            };

            IHttpActionResult response = await _controller.GetAsync();
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out Note[] actualNotes);

            Assert.Multiple(() =>
            {
                Assert.That(actualNotes.Length, Is.EqualTo(expectedNotes.Length));
                Assert.That(executedResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNotes, Is.EqualTo(expectedNotes).UsingNoteComparer());
            });
        }

        [Test]
        public async Task GetAsync_FindNoteById()
        {
            var noteId = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2");
            var expectedNote = new Note { Text = "First note", Id = noteId };

            IHttpActionResult response = await _controller.GetAsync(noteId);
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out Note actualNote);

            Assert.Multiple(() =>
            {
                Assert.That(expectedNote, Is.EqualTo(actualNote).UsingNoteComparer());
                Assert.That(executedResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            });
        }

        [Test]
        public async Task PostAsync_AddNewValidNote()
        {
            const string id = "ebcb3d81-af4e-428f-a22d-e7852d70d3a0";
            var expectedUri = new Uri($"http://test/{id}/test");
            var expectedNote = new Note { Text = "Second note", Id = new Guid(id) };

            IHttpActionResult response = await _controller.PostAsync(new Note { Text = "test text" });
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out Note actualNote);

            Assert.Multiple(() =>
            {
                Assert.That(executedResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(executedResponse.Headers.Location, Is.EqualTo(expectedUri));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        [Test]
        public async Task PutAsync_UpdateNote()
        {
            var updatedText = "Updated note";
            var expectedNote = new Note { Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1") };

            IHttpActionResult response =
                await _controller.PutAsync(new Note { Text = updatedText, Id = Guid.Parse("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2") });
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out Note actualNote);

            Assert.Multiple(() =>
            {
                Assert.That(executedResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        [Test]
        public async Task DeleteAsync_DeleteNoteById()
        {
            var expectedNote = new Note { Text = "Fourth note", Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59") };

            IHttpActionResult response = await _controller.DeleteAsync(Guid.NewGuid());
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out Note actualNote);

            Assert.Multiple(() =>
            {
                Assert.That(executedResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }
    }
}
