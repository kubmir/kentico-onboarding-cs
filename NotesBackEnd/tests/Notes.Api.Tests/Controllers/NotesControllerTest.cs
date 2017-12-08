using System;
using System.Collections.Generic;
using System.Net;
using Notes.Api.Controllers;
using NUnit.Framework;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Notes.Api.Tests.Comparers;
using Notes.Contracts.Model;

namespace Notes.Api.Tests.Controllers
{
    [TestFixture]
    internal class NotesControllerTest
    {
        private NotesController _controller;

        [SetUp]
        public void Init()
        {
            _controller = new NotesController
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://test")
                }
            };

            _controller.Configuration.Routes.MapHttpRoute(
                name: "Notes",
                routeTemplate: "{id}/test");
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

            var (actualNotes, responseMessage) = await GetExecutedResponse<IEnumerable<Note>>(_controller.GetAsync);

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNotes, Is.EqualTo(expectedNotes).UsingNoteComparer());
            });
        }

        [Test]
        public async Task GetAsync_FindNoteById()
        {
            var noteId = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2");
            var expectedNote = new Note { Text = "First note", Id = noteId };

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(() => _controller.GetAsync(noteId));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        [Test]
        public async Task PostAsync_AddNewValidNote()
        {
            const string id = "ebcb3d81-af4e-428f-a22d-e7852d70d3a0";
            var expectedUri = new Uri($"http://test/{id}/test");
            var expectedNote = new Note { Text = "Second note", Id = new Guid(id) };

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(() => _controller.PostAsync(new Note { Text = "test text" }));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(responseMessage.Headers.Location, Is.EqualTo(expectedUri));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        [Test]
        public async Task PutAsync_UpdateNote()
        {
            var updatedText = "Updated note";
            var expectedNote = new Note { Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1") };

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(()
                => _controller.PutAsync(new Note { Text = updatedText, Id = Guid.Parse("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2") }));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        [Test]
        public async Task DeleteAsync_DeleteNoteById()
        {
            var expectedNote = new Note { Text = "Fourth note", Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59") };

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(() => _controller.DeleteAsync(Guid.NewGuid()));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        internal async Task<(T ActualContent, HttpResponseMessage ResponseMessage)> GetExecutedResponse<T>(Func<Task<IHttpActionResult>> controllerFunction)
        {
            IHttpActionResult response = await controllerFunction();
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out T actualContent);

            return (actualContent, executedResponse);
        }
    }
}
