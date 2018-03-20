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
            MockedRetrievalService
                .Exists(Guid.Empty)
                .Returns(returnThis: false);

            var (_, responseMessage) = await GetExecutedResponse<Note>(() => Controller.DeleteAsync(Guid.Empty));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                Assert.That(Controller.ModelState.Count, Is.EqualTo(expected: 0));
            });
        }
    }
}
