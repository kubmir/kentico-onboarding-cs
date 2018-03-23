using System;
using System.Net;
using System.Threading.Tasks;
using Notes.Comparers.NoteComparers;
using Notes.Contracts.Model;
using NSubstitute;
using NUnit.Framework;

namespace Notes.Api.Tests.Controllers.NoteController
{
    [TestFixture]
    internal class DeleteAsyncTests : NoteControllerTestsBase
    {
        [Test]
        public async Task DeleteAsync_DeleteNoteById_DeletedNoteReturned()
        {
            MockedNoteRepository
                .DeleteByIdAsync(Note4.Id)
                .Returns(Note4);

            MockedRetrievalService
                .Exists(Note4.Id)
                .Returns(returnThis: true);


            var (actualNote, responseMessage) =
                await GetExecutedResponse<Note>(() => Controller.DeleteAsync(Note4.Id));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(expected: 0));
                Assert.That(actualNote, Is.EqualTo(Note4).UsingNoteComparer());
            });
        }

        [Test]
        public async Task DeleteAsync_DeleteNonExistingNote_NotFoundReturned()
        {
            var unknownId = new Guid("2c733ad6-2687-4558-8702-3c39d34c0366");

            MockedRetrievalService
                .Exists(unknownId)
                .Returns(returnThis: false);

            var (_, responseMessage) = await GetExecutedResponse<Note>(() => Controller.DeleteAsync(unknownId));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(expected: 0));
            });
        }

        [Test]
        public async Task DeleteAsync_DeleteEmptyId_BadRequestReturned()
        {
            var (_, responseMessage) = await GetExecutedResponse<Note>(() => Controller.DeleteAsync(Guid.Empty));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(expected: 0));
            });
        }
    }
}
