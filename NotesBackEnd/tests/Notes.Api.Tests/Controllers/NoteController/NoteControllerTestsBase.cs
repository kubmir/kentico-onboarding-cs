using System;
using Notes.Api.Controllers;
using NUnit.Framework;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Notes.Contracts.ApiServices;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;
using NSubstitute;

namespace Notes.Api.Tests.Controllers.NoteController
{
    internal class NoteControllerTestsBase
    {
        protected const String NameOfNote = "note";

        protected static readonly Note Note1 = new Note
        {
            Text = "First note",
            Id = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2"),
            CreationDate = DateTime.MinValue,
            LastModificationDate = DateTime.MaxValue
        };

        protected static readonly Note Note2 = new Note
        {
            Text = "Second note",
            Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"),
            CreationDate = DateTime.MinValue,
            LastModificationDate = DateTime.MaxValue
        };

        protected static readonly Note Note3 = new Note
        {
            Text = "Third note",
            Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1"),
            CreationDate = DateTime.MinValue,
            LastModificationDate = DateTime.MaxValue
        };

        protected static readonly Note Note4 = new Note
        {
            Text = "Fourth note",
            Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59"),
            CreationDate = DateTime.MinValue,
            LastModificationDate = DateTime.MaxValue
        };

        protected static readonly Note[] AllNotes =
        {
            Note1,
            Note2,
            Note3,
            Note4
        };

        protected static readonly Guid UnknownId = new Guid("67b8d269-96e0-4928-983c-86659acd47cb");

        protected static readonly Note Note2Dto = new Note { Text = "test text" };

        protected IUrlLocationHelper MockedLocationHelper = Substitute.For<IUrlLocationHelper>();
        protected ICreationService MockedCreationService = Substitute.For<ICreationService>();
        protected IUpdateService MockedUpdateService = Substitute.For<IUpdateService>();
        protected INotesRepository MockedNoteRepository = Substitute.For<INotesRepository>();
        protected IRetrievalService MockedRetrievalService = Substitute.For<IRetrievalService>();
        protected NotesController Controller;

        [SetUp]
        public void SetUpController()
        {
            Controller = new NotesController(MockedLocationHelper, MockedCreationService, MockedUpdateService,
                MockedNoteRepository, MockedRetrievalService)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage()
            };
        }

        protected async Task<(T ActualContent, HttpResponseMessage ResponseMessage)> GetExecutedResponse<T>(
            Func<Task<IHttpActionResult>> controllerFunction)
        {
            var response = await controllerFunction();
            var executedResponse = await response.ExecuteAsync(CancellationToken.None);
            executedResponse.TryGetContentValue(out T actualContent);

            return (actualContent, executedResponse);
        }
    }
}
