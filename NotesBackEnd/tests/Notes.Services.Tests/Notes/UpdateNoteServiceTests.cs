using System;
using System.Globalization;
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
    internal class UpdateNoteServiceTests
    {
        private static readonly Note Note = new Note { Text = "Third note", Id = new Guid("599442c0-ae28-4157-9a3f-0491bb4ba6c1"), CreationDate = DateTime.MinValue, LastModificationDate = DateTime.MaxValue };
        private static readonly DateTime TestDateTime = DateTime.ParseExact("21/10/2017 18:00", "g", new CultureInfo("fr-FR"));

        private IUpdateNoteService _updateService;

        [SetUp]
        public void SetUp()
        {
            var mockedRepository = MockNotesRepository();
            var mockedDateService = MockDateService();

            _updateService = new UpdateNoteService(mockedDateService, mockedRepository);
        }

        [Test]
        public async Task UpdateNoteAsync_UpdateCorrectNote()
        {
            var expectedNote = Note;

            var actualNote = await _updateService.UpdateNoteAsync(Note);

            Assert.That(actualNote, Is.EqualTo(expectedNote));
        }

        private INotesRepository MockNotesRepository()
        {
            var mockedRepository = Substitute.For<INotesRepository>();

            mockedRepository
                .UpdateNoteAsync(Note.Id, Arg.Is<Note>(repositoryNote => repositoryNote.Id == Note.Id))
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
    }
}
