using System;
using System.Globalization;
using System.Threading.Tasks;
using Notes.Comparers.NoteComparers;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Wrappers;
using Notes.Services.Notes;
using NSubstitute;
using NUnit.Framework;

namespace Notes.Services.Tests.Notes
{
    internal class UpdateServiceTests
    {
        private static readonly Note Note = new Note
        {
            Text = "Third note",
            Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1"),
            CreationDate = DateTime.MinValue,
            LastModificationDate = DateTime.MaxValue
        };

        private static readonly DateTime TestDateTime = DateTime.ParseExact("21/10/2017 18:00", "g", new CultureInfo("fr-FR"));
        private IUpdateService _updateService;

        [SetUp]
        public void SetUp()
        {
            var mockedRepository = MockNotesRepository();
            var mockedDateWrapper = MockDateWrapper();
            var mockedRetrieveService = MockRetrieveService();

            _updateService = new UpdateService(mockedDateWrapper, mockedRepository, mockedRetrieveService);
        }

        [Test]
        public async Task UpdateAsync_CorrectNote_NoteForUpdateReturned()
        {
            var expectedNote = new Note
            {
                Text = Note.Text,
                Id = Note.Id,
                CreationDate = Note.CreationDate,
                LastModificationDate = TestDateTime
            };

            var actualNote = await _updateService.UpdateAsync(Note.Id, Note);

            Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
        }

        private INotesRepository MockNotesRepository()
        {
            var mockedRepository = Substitute.For<INotesRepository>();

            mockedRepository
                .UpdateAsync(Arg.Any<Guid>(), Arg.Any<Note>())
                .Returns(parameters => parameters.Arg<Note>());

            return mockedRepository;
        }

        private IDateWrapper MockDateWrapper()
        {
            var mockedDateWrapper = Substitute.For<IDateWrapper>();

            mockedDateWrapper
                .GetCurrentDateTime()
                .Returns(TestDateTime);

            return mockedDateWrapper;
        }

        private IRetrievalService MockRetrieveService()
        {
            var mockedRetrieveService = Substitute.For<IRetrievalService>();

            mockedRetrieveService
                .GetByIdAsync(Note.Id)
                .Returns(Note);

            return mockedRetrieveService;
        }
    }
}
