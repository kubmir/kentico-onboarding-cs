using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Notes.Comparers.NoteComparers;
using Notes.Contracts.Model;
using NSubstitute;
using NUnit.Framework;

namespace Notes.Api.Tests.Controllers.NoteController
{
    [TestFixture]
    internal class PostAsyncTests : NoteControllerTestsBase
    {
        [Test]
        public async Task PostAsync_AddNewValidNote_CreatedNoteReturned()
        {
            MockedCreationService
                .CreateAsync(Note2Dto)
                .Returns(Note2);

            MockedLocationHelper
                .GetNotesUrlWithId(Note2.Id)
                .Returns($"http://test/{Note2.Id}/test");


            var expectedUri = new Uri($"http://test/{Note2.Id}/test");

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PostAsync(Note2Dto));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(responseMessage.Headers.Location, Is.EqualTo(expectedUri));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(0));
                Assert.That(actualNote, Is.EqualTo(Note2).UsingNoteComparer());
            });
        }

        [Test]
        public async Task PostAsync_AddNewNoteWithEmptyText_BadRequestReturned()
        {
            var noteWithEmptyText = new Note { Text = String.Empty };

            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PostAsync(noteWithEmptyText));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(1));
                Assert.That(Controller.ModelState.First().Key, Is.EqualTo(nameof(noteWithEmptyText.Text)));
            });
        }

        [Test]
        public async Task PostAsync_AddNewNullNote_BadRequestReturned()
        {
            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PostAsync(null));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(1));
                Assert.That(Controller.ModelState.First().Key, Is.EqualTo(NameOfNote));
            });
        }

        [Test]
        public async Task PostAsync_AddNewNoteWithId_BadRequestReturned()
        {
            var noteWithId = new Note { Id = Note1.Id, Text = Note1.Text };

            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PostAsync(noteWithId));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(1));
                Assert.That(Controller.ModelState.First().Key, Is.EqualTo(nameof(noteWithId.Id)));
            });
        }

        [Test]
        public async Task PostAsync_AddNewNoteWithCreationDate_BadRequestReturned()
        {
            var invalidNote = new Note { CreationDate = DateTime.MaxValue, Text = "txt", Id = Guid.Empty };

            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PostAsync(invalidNote));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(1));
                Assert.That(Controller.ModelState.First().Key, Is.EqualTo(nameof(invalidNote.CreationDate)));
            });
        }

        [Test]
        public async Task PostAsync_AddNewNoteWithLastModificationDate_BadRequestReturned()
        {
            var invalidNote = new Note { LastModificationDate = DateTime.MaxValue, Text = "txt", Id = Guid.Empty };

            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PostAsync(invalidNote));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(1));
                Assert.That(Controller.ModelState.First().Key, Is.EqualTo(nameof(invalidNote.LastModificationDate)));
            });
        }
    }
}
