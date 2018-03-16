using System;
using System.Linq;
using System.Threading.Tasks;
using Notes.Comparers.NoteComparers;
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
        private static readonly Note Note = new Note
        {
            Text = "Test note",
            Id = new Guid("ebcb3d81-af4e-428f-a22d-e7852d70d3a0"),
            CreationDate = DateTime.MinValue,
            LastModificationDate = DateTime.MaxValue
        };

        private INotesRepository _mockedNotesRepository;
        private IGetNoteService _getService;

        [SetUp]
        public void SetUp()
        {
            _mockedNotesRepository = MockNotesRepository();

            _getService = new GetNoteService(_mockedNotesRepository);
        }

        [Test]
        public async Task GetByIdAsync_FromRepository_CorrectNoteReturned()
        {
            var expectedNote = Note;

            var actualNote = await _getService.GetByIdAsync(Note.Id);

            Assert.That(actualNote, Is.EqualTo(expectedNote).UsingNoteComparer());
        }

        [Test]
        public async Task GetByIdAsync_FromCache_CorrectNoteReturned()
        {
            var expectedNote = Note;

            var noteFromRepository = await _getService.GetByIdAsync(Note.Id);
            var noteFromCache = await _getService.GetByIdAsync(Note.Id);

            Assert.Multiple(() =>
            {
                Assert.That(noteFromRepository, Is.EqualTo(expectedNote).UsingNoteComparer());
                Assert.That(noteFromCache, Is.EqualTo(expectedNote).UsingNoteComparer());
                Assert.That(_mockedNotesRepository.ReceivedCalls().Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task ExistsAsync_ExistingNote_TrueReturned()
        {
            var isExisting = await _getService.Exists(Note.Id);

            Assert.That(isExisting, Is.True);
        }

        [Test]
        public async Task ExistsAsync_ExistingNoteFromCache_TrueReturned()
        {
            var isExistingFromRepository = await _getService.Exists(Note.Id);
            var isExistingFromCache = await _getService.Exists(Note.Id);

            Assert.Multiple(() =>
            {
                Assert.That(isExistingFromRepository, Is.True);
                Assert.That(isExistingFromCache, Is.True);
                Assert.That(_mockedNotesRepository.ReceivedCalls().Count(), Is.EqualTo(1));
            });
        }

        [Test]
        public async Task ExistsAsync_NotExistingNote_FalseReturned()
        {
            var isExisting = await _getService.Exists(Guid.Empty);

            Assert.That(isExisting, Is.False);
        }

        private INotesRepository MockNotesRepository()
        {
            var mockedRepository = Substitute.For<INotesRepository>();

            mockedRepository
                .GetByIdAsync(Note.Id)
                .Returns(Note);

            return mockedRepository;
        }
    }
}
