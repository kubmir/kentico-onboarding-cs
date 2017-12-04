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
        private NoteEqualityComparer _noteEqualityComparer;

        [SetUp]
        public void Init()
        {
            _controller = new NoteController();
            _controller.Configuration = new HttpConfiguration();
            _controller.Request = new HttpRequestMessage();
            _noteEqualityComparer = new NoteEqualityComparer();
        }

        [Test]
        public async Task Get_FindNotesAsync()
        {
            var expectedNotes = new[]
            {
                new Note("First note", new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2"), false),
                new Note("Second note", new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"), false),
                new Note("Third note", new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1"), false),
                new Note("Fourth note", new Guid("4785546e-824d-42a4-900b-e656f19ffb59"), false)
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
            var noteId = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2");
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

        [Test]
        public async Task Post_AddNewNoteAsync()
        {
            var text = "New note text";
            var expectedNotes = new[]
            {
                new Note("First note", new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2"), false),
                new Note("Second note", new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"), false),
                new Note("Third note", new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1"), false),
                new Note("Fourth note", new Guid("4785546e-824d-42a4-900b-e656f19ffb59"), false),
                new Note(text, new Guid("d0a0f5bc-0f46-43f0-ab4b-06860c3ea19c"), false)
            };

            IHttpActionResult response = await _controller.AddNoteAsync(text);
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out Note[] actualNotes);

            Assert.Multiple(() =>
            {
                Assert.That(HttpStatusCode.OK, Is.EqualTo(executedResponse.StatusCode));
                Assert.That(expectedNotes, Is.EqualTo(actualNotes).Using(_noteEqualityComparer));
            });
        }

        [Test]
        public async Task Put_UpdateNoteAsync()
        {
            var updatedText = "Updated note";
            var expectedNotes = new[]
            {
                new Note(updatedText, new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2"), false),
                new Note("Second note", new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"), false),
                new Note("Third note", new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1"), false),
                new Note("Fourth note", new Guid("4785546e-824d-42a4-900b-e656f19ffb59"), false),
            };

            IHttpActionResult response =
                await _controller.UpdateNoteAsync(Guid.Parse("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2"), updatedText);
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out Note[] actualNotes);

            Assert.Multiple(() =>
            {
                Assert.That(HttpStatusCode.OK, Is.EqualTo(executedResponse.StatusCode));
                Assert.That(expectedNotes, Is.EqualTo(actualNotes).Using(_noteEqualityComparer));
            });
        }
    }
}
