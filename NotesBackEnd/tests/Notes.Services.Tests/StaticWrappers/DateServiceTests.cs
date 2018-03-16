using Notes.Contracts.Services.Utils;
using Notes.Services.StaticWrappers;
using NUnit.Framework;

namespace Notes.Services.Tests.StaticWrappers
{
    internal class DateServiceTests
    {
        private IDateService _dateService;

        [SetUp]
        public void SetUp()
            => _dateService = new DateService();

        [Test]
        public void GetCurrentDateTime_CalledTwice_SecondReturnedTimeBiggerThanFirstReturned()
        {
            var firstTime = _dateService.GetCurrentDateTime();
            var secondTime = _dateService.GetCurrentDateTime();

            Assert.That(secondTime, Is.GreaterThan(firstTime));
        }
    }
}
