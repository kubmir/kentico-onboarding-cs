using System;
using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Date;
using Notes.Contracts.Services.Notes;
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

        private INotesServices _notesServices;

        [SetUp]
        public void SetUp()
        {
            var mockedRepository = MockNotesRepository();
            var mockedDateService = MockDateService();

            _notesServices = new NotesServices(mockedDateService, mockedRepository);
        }

        [Test]
        public async Task CreateNoteAsync_CreateCorrectNotes()
        {
            var expectedNote = Note2;
            expectedNote.CreationDate = DateTime.Parse("21.10.2017 18:00:01");
            expectedNote.LastModificationDate = DateTime.Parse("21.10.2017 18:00:01");

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

        private INotesRepository MockNotesRepository()
        {
            var mockedRepository = Substitute.For<INotesRepository>();
            mockedRepository.GetAllNotesAsync().Returns(AllNotes);
            mockedRepository.GetNoteByIdAsync(Arg.Any<Guid>()).Returns(Note1);
            mockedRepository.CreateNoteAsync(Arg.Any<Note>()).Returns(Note2);
            mockedRepository.UpdateNoteAsync(Arg.Any<Note>()).Returns(Note3);
            mockedRepository.DeleteNoteByIdAsync(Arg.Any<Guid>()).Returns(Note4);

            return mockedRepository;
        }

        private IDateService MockDateService()
        {
            var mockedDateService = Substitute.For<IDateService>();
            mockedDateService.GetCurrentDateTime().Returns(DateTime.Parse("21.10.2017 18:00:01"));

            return mockedDateService;
        }
    }
}
