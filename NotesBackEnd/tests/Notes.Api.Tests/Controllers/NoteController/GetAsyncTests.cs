using System;
using System.Collections.Generic;
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
    internal class GetAsyncTests : NoteControllerTestsBase
    {
        [Test]
        public async Task GetAsync_FindAllNotes_AllPersistedNotesReturned()
        {
            MockedNoteRepository
                .GetAllAsync()
                .Returns(AllNotes);

            var expectedNotes = AllNotes;

            var (actualNotes, responseMessage) = await GetExecutedResponse<IEnumerable<Note>>(Controller.GetAsync);

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNotes, Is.EqualTo(expectedNotes).UsingNoteComparer());
            });
        }

        [Test]
        public async Task GetAsync_FindNoteById_CorrectNoteReturned()
        {
            MockedRetrievalService
                .GetByIdAsync(Note1.Id)
                .Returns(Note1);

            MockedRetrievalService
                .Exists(Note1.Id)
                .Returns(returnThis: true);

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.GetAsync(Note1.Id));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(expected: 0));
                Assert.That(actualNote, Is.EqualTo(Note1).UsingNoteComparer());
            });
        }

        [Test]
        public async Task GetAsync_NonExistingId_NotFoundReturned()
        {
            MockedRetrievalService
                .Exists(UnknownId)
                .Returns(returnThis: false);

            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.GetAsync(UnknownId));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(expected: 0));
            });
        }

        [Test]
        public async Task GetAsync_EmptyId_BadRequestReturned()
        {
            var (_, responseMessage) = await GetExecutedResponse<Note>(()
                => Controller.GetAsync(Guid.Empty));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(expected: 1));
                Assert.That(Controller.ModelState.First().Key, Is.EqualTo("id"));
            });
        }
    }
}
