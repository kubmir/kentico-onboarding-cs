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
    internal class PutAsyncTests : NoteControllerTestsBase
    {
        [Test]
        public async Task PutAsync_UpdateNote_NoteForUpdateReturned()
        {
            MockedUpdateService
                .UpdateAsync(Note3.Id, Note3)
                .Returns(Note3);

            MockedRetrievalService
                .Exists(Note3.Id)
                .Returns(true);

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PutAsync(Note3.Id, Note3));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(0));
                Assert.That(actualNote, Is.EqualTo(Note3).UsingNoteComparer());
            });
        }

        [Test]
        public async Task PutAsync_UpdateWithDifferentIdInUrlAndBody_ConflictReturned()
        {
            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PutAsync(Note3.Id, Note2));

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task PutAsync_UpdateNullNote_BadRequestReturned()
        {
            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PutAsync(Note3.Id, null));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(1));
                Assert.That(Controller.ModelState.First().Key, Is.EqualTo(NameOfNote));
            });
        }

        [Test]
        public async Task PutAsync_UpdateNoteWithEmptyText_BadRequestReturned()
        {
            var mockedId = new Guid("67b8d269-96e0-4928-983c-86659acd47cb");
            var noteWithEmptyText = new Note {Text = String.Empty, Id = mockedId};

            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PutAsync(mockedId, noteWithEmptyText));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(1));
                Assert.That(Controller.ModelState.First().Key, Is.EqualTo(nameof(noteWithEmptyText.Text)));
            });
        }

        [Test]
        public async Task PutAsync_UpdateNoteWithUnknownId_NotFoundReturned()
        {
            Note1.Id = UnknownId;

            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PutAsync(UnknownId, Note1));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(0));
            });
        }


        [Test]
        public async Task PutAsync_CreateNewNoteUsingPost_CreatedNoteReturned()
        {
            MockedCreationService
                .CreateAsync(Note2Dto)
                .Returns(Note2);

            MockedLocationHelper
                .GetNotesUrlWithId(Note2.Id)
                .Returns($"http://test/{Note2.Id}/test");

            var expectedUri = new Uri($"http://test/{Note2.Id}/test");

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PutAsync(Guid.Empty, Note2Dto));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(responseMessage.Headers.Location, Is.EqualTo(expectedUri));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(0));
                Assert.That(actualNote, Is.EqualTo(Note2).UsingNoteComparer());
            });
        }

        [Test]
        public async Task PutAsync_CreateNewNoteUsingPost_PostBadRequestReturned()
        {
            Note1.Id = Guid.Empty;
            Note1.CreationDate = DateTime.MaxValue;

            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.PutAsync(Guid.Empty, Note1));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(2));
                Assert.That(Controller.ModelState.Keys, Contains.Item(nameof(Note1.CreationDate)));
                Assert.That(Controller.ModelState.Keys, Contains.Item(nameof(Note1.LastModificationDate)));
            });
        }
    }
}
