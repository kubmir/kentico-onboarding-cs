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
using Notes.Contracts.ApiServices;
using Notes.Contracts.Model;
using Notes.Contracts.Services.Notes;
using NSubstitute;

namespace Notes.Api.Tests.Controllers
{
    [TestFixture]
    internal class NotesControllerTest
    {
        private static readonly Note Note1 = new Note { Text = "First note", Id = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly Note Note2 = new Note { Text = "Second note", Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly Note Note3 = new Note { Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly Note Note4 = new Note { Text = "Fourth note", Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly Note[] AllNotes = { Note1, Note2, Note3, Note4 };

        private static readonly Note Note2Dto = new Note { Text = "test text" };

        private NotesController _controller;

        [SetUp]
        public void Init()
        {
            var mockedLocationHelper = MockLocationHelper();
            var mockedNotesServices = MockNotesServices();

            _controller = new NotesController(mockedLocationHelper, mockedNotesServices)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
        }

        [Test]
        public async Task GetAsync_FindAllNotes()
        {
            var expectedNotes = AllNotes;

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
            var expectedNote = Note1;
            var noteId = expectedNote.Id;

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(() => _controller.GetAsync(noteId));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        [Test]
        public async Task GetAsync_NonExistingId()
        {
            var (_, responseMessage) = await GetExecutedResponse<Note>(() => _controller.GetAsync(Guid.Empty));

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }


        [Test]
        public async Task PostAsync_AddNewValidNote()
        {
            var expectedNote = Note2;
            string id = expectedNote.Id.ToString();
            var expectedUri = new Uri($"http://test/{id}/test");

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(() => _controller.PostAsync(Note2Dto));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(responseMessage.Headers.Location, Is.EqualTo(expectedUri));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        [Test]
        public async Task PostAsync_AddNewNoteWithEmptyText()
        {
            var (_, responseMessage) = await GetExecutedResponse<Note>(() => _controller.PostAsync(new Note { Text = "" }));

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task PostAsync_AddNewNullNote()
        {
            var (_, responseMessage) = await GetExecutedResponse<Note>(() => _controller.PostAsync(null));

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task PostAsync_AddNewNoteWithId()
        {
            var (_, responseMessage) = await GetExecutedResponse<Note>(() => _controller.PostAsync(Note1));

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task PostAsync_AddNewNoteWithCreationDate()
        {
            var invalidNote = new Note { CreationDate = DateTime.MaxValue, Text = "txt", Id = Guid.Empty };

            var (_, responseMessage) = await GetExecutedResponse<Note>(() => _controller.PostAsync(invalidNote));

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task PostAsync_AddNewNoteWithLastModificationDate()
        {
            var invalidNote = new Note { LastModificationDate = DateTime.MaxValue, Text = "txt", Id = Guid.Empty };

            var (_, responseMessage) = await GetExecutedResponse<Note>(() => _controller.PostAsync(invalidNote));

            Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task PutAsync_UpdateNote()
        {
            var expectedNote = Note3;

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(()
                => _controller.PutAsync(Note3.Id, Note3));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        [Test]
        public async Task DeleteAsync_DeleteNoteById()
        {
            var expectedNote = Note4;

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(() => _controller.DeleteAsync(Guid.NewGuid()));

            Assert.Multiple(() =>
            {
                Assert.That(responseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
            });
        }

        private async Task<(T ActualContent, HttpResponseMessage ResponseMessage)> GetExecutedResponse<T>(Func<Task<IHttpActionResult>> controllerFunction)
        {
            IHttpActionResult response = await controllerFunction();
            HttpResponseMessage executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out T actualContent);

            return (actualContent, executedResponse);
        }

        private IUrlLocationHelper MockLocationHelper()
        {
            var mockedLocationHelper = Substitute.For<IUrlLocationHelper>();
            mockedLocationHelper.GetUrlWithId(Arg.Any<String>(), Arg.Any<Guid>()).Returns("http://test/ebcb3d81-af4e-428f-a22d-e7852d70d3a0/test");

            return mockedLocationHelper;
        }

        private INotesServices MockNotesServices()
        {
            var mockedNotesServices = Substitute.For<INotesServices>();

            mockedNotesServices
                .CreateNoteAsync(Note2Dto)
                .Returns(Note2);

            mockedNotesServices
                .GetAllNotesAsync()
                .Returns(AllNotes);

            mockedNotesServices
                .GetNoteAsync(Note1.Id)
                .Returns(Note1);

            mockedNotesServices
                .UpdateNoteAsync(Arg.Any<Note>())
                .Returns(Note3);

            mockedNotesServices
                .DeleteNoteAsync(Arg.Any<Guid>())
                .Returns(Note4);

            return mockedNotesServices;
        }
    }
}
