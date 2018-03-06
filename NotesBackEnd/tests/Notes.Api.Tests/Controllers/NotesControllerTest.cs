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
using Notes.Contracts.Repository;
using NSubstitute;

namespace Notes.Api.Tests.Controllers
{
    [TestFixture]
    internal class NotesControllerTest
    {
        private static readonly Note Note1 = new Note {Text = "First note", Id = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2")};
        private static readonly Note Note2 = new Note {Text = "Second note", Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0")};
        private static readonly Note Note3 = new Note {Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1")};
        private static readonly Note Note4 = new Note {Text = "Fourth note", Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59")};
        private static readonly Note[] AllNotes = {Note1, Note2, Note3, Note4};

        private NotesController _controller;

        [SetUp]
        public void Init()
        {
            var mockedNotesRepository = MockNotesRepository();
            var mockedLocationHelper = MockLocationHelper();

            _controller = new NotesController(mockedNotesRepository, mockedLocationHelper)
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
        public async Task PostAsync_AddNewValidNote()
        {
            var expectedNote = Note2;
            string id = expectedNote.Id.ToString();
            var expectedUri = new Uri($"http://test/{id}/test");

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(() => _controller.PostAsync(Note2));

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

            var (actualNote, responseMessage) = await GetExecutedResponse<Note>(() => _controller.DeleteAsync(Note4.Id));

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

        private INotesRepository MockNotesRepository()
        {
            var mockedRepository = Substitute.For<INotesRepository>();
            mockedRepository.GetAllNotesAsync().Returns(AllNotes);
            mockedRepository.GetNoteByIdAsync(Note1.Id).Returns(Note1);
            mockedRepository.CreateNoteAsync(Note2).Returns(Note2);
            mockedRepository.UpdateNoteAsync(Note3).Returns(Note3);
            mockedRepository.DeleteNoteByIdAsync(Note4.Id).Returns(Note4);

            return mockedRepository;
        }

        private IUrlLocationHelper MockLocationHelper()
        {
            var mockedId = "ebcb3d81-af4e-428f-a22d-e7852d70d3a0";
            var mockedLocationHelper = Substitute.For<IUrlLocationHelper>();
            mockedLocationHelper.GetNotesUrlWithId(Guid.Parse(mockedId)).Returns($"http://test/{mockedId}/test");

            return mockedLocationHelper;
        }
    }
}
