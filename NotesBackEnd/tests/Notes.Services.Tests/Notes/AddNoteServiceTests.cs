using System;
using System.Globalization;
using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Date;
using Notes.Contracts.Services.Notes;
using Notes.Contracts.Services.Utils;
using Notes.Services.Notes;
using NSubstitute;
using NUnit.Framework;

namespace Notes.Services.Tests.Notes
{
    class AddNoteServiceTests
    {
        private static readonly Note Note = new Note { Text = "Test note", Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly Note NoteDto = new Note { Text = "test text" };
        private static readonly DateTime TestDateTime = DateTime.ParseExact("21/10/2017 18:00", "g", new CultureInfo("fr-FR"));
   
        private IAddNoteService _addService;

        [SetUp]
        public void SetUp()
        {
            var mockedRepository = MockNotesRepository();
            var mockedDateService = MockDateService();
            var mockedGuidService = MockGuidService();

            _addService = new AddNoteService(mockedDateService, mockedRepository, mockedGuidService);
        }

        [Test]
        public async Task CreateNoteAsync_CreateCorrectNotes()
        {
            var expectedNote = Note;
            expectedNote.CreationDate = TestDateTime;
            expectedNote.LastModificationDate = TestDateTime;

            var actualNote = await _addService.CreateNoteAsync(NoteDto);

            Assert.That(actualNote, Is.EqualTo(expectedNote));
        }

        private INotesRepository MockNotesRepository()
        {
            var mockedRepository = Substitute.For<INotesRepository>();

            mockedRepository
                .CreateNoteAsync(NoteDto)
                .Returns(Note);

            return mockedRepository;
        }

        private IDateService MockDateService()
        {
            var mockedDateService = Substitute.For<IDateService>();

            mockedDateService
                .GetCurrentDateTime()
                .Returns(TestDateTime);

            return mockedDateService;
        }

        private IGuidService MockGuidService()
        {
            var mockedGuidService = Substitute.For<IGuidService>();

            mockedGuidService
                .GetNewGuid()
                .Returns(Note.Id);

            return mockedGuidService;
        }
    }
}
