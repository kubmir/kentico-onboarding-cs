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
    [TestFixture]
    class NotesServicesTest
    {
        private static readonly Note Note1 = new Note { Text = "First note", Id = new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly Note Note2 = new Note { Text = "Second note", Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly Note Note3 = new Note { Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly Note Note4 = new Note { Text = "Fourth note", Id = new Guid("4785546e-824d-42a4-900b-e656f19ffb59"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly Note[] AllNotes = { Note1, Note2, Note3, Note4 };

        private static readonly Note Note2Dto = new Note { Text = "test text" };

        private static readonly DateTime TestDateTime =
            DateTime.ParseExact("21/10/2017 18:00", "g", new CultureInfo("fr-FR"));

        private INotesServices _notesServices;

        [SetUp]
        public void SetUp()
        {
            var mockedRepository = MockNotesRepository();
            var mockedDateService = MockDateService();
            var mockedGuidService = MockGuidService();

            _notesServices = new NotesServices(mockedDateService, mockedRepository, mockedGuidService);
        }

        [Test]
        public async Task CreateNoteAsync_CreateCorrectNotes()
        {
            var expectedNote = Note2;
            expectedNote.CreationDate = TestDateTime;
            expectedNote.LastModificationDate = TestDateTime;

            var actualNote = await _notesServices.CreateNoteAsync(Note2Dto);

            Assert.That(actualNote, Is.EqualTo(expectedNote));
        }

        [Test]
        public async Task GetNoteAsync_ReturnCorrectNote()
        {
            var expectedNote = Note1;

            var actualNote = await _notesServices.GetNoteAsync(Note1.Id);

            Assert.That(actualNote, Is.EqualTo(expectedNote));
        }

        [Test]
        public async Task GetAllNotesAsync_ReturnCorrectNotes()
        {
            var expectedNotes = AllNotes;

            var actualNotes = await _notesServices.GetAllNotesAsync();

            Assert.That(actualNotes, Is.EqualTo(expectedNotes));
        }

        [Test]
        public async Task DeleteNoteAsync_DeleteCorrectNote()
        {
            var expectedNote = Note4;

            var actualNote = await _notesServices.DeleteNoteAsync(Note4.Id);

            Assert.That(actualNote, Is.EqualTo(expectedNote));
        }

        [Test]
        public async Task UpdateNoteAsync_UpdateCorrectNote()
        {
            var expectedNote = Note3;

            var actualNote = await _notesServices.UpdateNoteAsync(Note3.Id, Note3);

            Assert.That(actualNote, Is.EqualTo(expectedNote));
        }

        private INotesRepository MockNotesRepository()
        {
            var mockedRepository = Substitute.For<INotesRepository>();

            mockedRepository
                .GetAllNotesAsync()
                .Returns(AllNotes);
            mockedRepository
                .GetNoteByIdAsync(Note1.Id)
                .Returns(Note1);
            mockedRepository
                .CreateNoteAsync(Note2Dto)
                .Returns(Note2);
            mockedRepository
                .UpdateNoteAsync(Note3.Id, Note3)
                .Returns(Note3);
            mockedRepository
                .DeleteNoteByIdAsync(Note4.Id)
                .Returns(Note4);

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
                .Returns(Note2.Id);

            return mockedGuidService;
        }
    }
}
