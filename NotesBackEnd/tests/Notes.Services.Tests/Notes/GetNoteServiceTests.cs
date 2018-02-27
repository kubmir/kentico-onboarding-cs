using System;
using System.Linq;
using System.Threading.Tasks;
using Notes.Contracts.Model;
using Notes.Contracts.Repository;
using Notes.Contracts.Services.Notes;
using Notes.Services.Notes;
using NSubstitute;
using NUnit.Framework;

namespace Notes.Services.Tests.Notes
{
    internal class GetNoteServiceTests
    {
        private static readonly Note Note = new Note { Text = "Test note", Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private INotesRepository _mockedNotesRepository;
        private IGetNoteService _getService;

        [SetUp]
        public void SetUp()
        {
            _mockedNotesRepository = MockNotesRepository();

            _getService = new GetNoteService(_mockedNotesRepository);
        }

        [Test]
        public async Task GetNoteByIdAsync_ReturnCorrectNote_FromRepository()
        {
            var expectedNote = Note;

            var actualNote = await _getService.GetNoteByIdAsync(Note.Id);

            Assert.That(actualNote, Is.EqualTo(expectedNote));
        }

        [Test]
        public async Task GetNoteByIdAsync_ReturnCorrectNote_FromCache()
        {
            var expectedNote = Note;

            var noteFromRepository = await _getService.GetNoteByIdAsync(Note.Id);
            var noteFromCache = await _getService.GetNoteByIdAsync(Note.Id);

            Assert.Multiple(() =>
            {
                Assert.That(noteFromRepository, Is.EqualTo(expectedNote));
                Assert.That(noteFromCache, Is.EqualTo(expectedNote));
                Assert.That(_mockedNotesRepository.ReceivedCalls().Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task IsNoteExistingAsync_ReturnTrueForExistingNote()
        {
            var isExisting = await _getService.IsNoteExistingAsync(Note.Id);

            Assert.That(isExisting, Is.True);
        }

        [Test]
        public async Task IsNoteExistingAsync_ReturnTrueForExistingNote_FromCache()
        {
            var isExistingFromRepository = await _getService.IsNoteExistingAsync(Note.Id);
            var isExistingFromCache = await _getService.IsNoteExistingAsync(Note.Id);

            Assert.Multiple(() =>
            {
                Assert.That(isExistingFromRepository, Is.True);
                Assert.That(isExistingFromCache, Is.True);
                Assert.That(_mockedNotesRepository.ReceivedCalls().Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task IsNoteExistingAsync_ReturnFalseForNotExistingNote()
        {
            var isExisting = await _getService.IsNoteExistingAsync(Guid.Empty);

            Assert.That(isExisting, Is.False);
        }

        private INotesRepository MockNotesRepository()
        {
            var mockedRepository = Substitute.For<INotesRepository>();

            mockedRepository
                .GetNoteByIdAsync(Note.Id)
                .Returns(Note);

            return mockedRepository;
        }
    }
}
