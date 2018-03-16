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
    internal class CreationServiceTests
    {
        private static readonly Note NoteDto = new Note { Text = "test text" };
        private static readonly DateTime TestDateTime = DateTime.ParseExact("21/10/2017 18:00", "g", new CultureInfo("fr-FR"));
        private static readonly Note CreatedNote = new Note
        {
            Text = NoteDto.Text,
            Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"),
            CreationDate = TestDateTime,
            LastModificationDate = TestDateTime
        };

        private ICreationService _addService;

        [SetUp]
        public void SetUp()
        {
            var mockedRepository = MockNotesRepository();
            var mockedDateWrapper = MockDateWrapper();
            var mockedGuidWrapper = MockGuidWrapper();

            _addService = new CreationService(mockedDateWrapper, mockedRepository, mockedGuidWrapper);
        }

        [Test]
        public async Task CreateAsync_CorrectNote_CreatedNoteReturned()
        {
            var expectedNote = CreatedNote;

            var actualNote = await _addService.CreateAsync(NoteDto);

            Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
        }

        private INotesRepository MockNotesRepository()
        {
            var mockedRepository = Substitute.For<INotesRepository>();

            mockedRepository
                .CreateAsync(Arg.Any<Note>())
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

        private IGuidWrapper MockGuidWrapper()
        {
            var mockedGuidWrapper = Substitute.For<IGuidWrapper>();

            mockedGuidWrapper
                .GetNew()
                .Returns(CreatedNote.Id);

            return mockedGuidWrapper;
        }
    }
}
