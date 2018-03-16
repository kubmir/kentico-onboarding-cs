using Notes.Contracts.Services.Wrappers;
using Notes.Services.Wrappers;
using NUnit.Framework;

namespace Notes.Services.Tests.StaticWrappers
{
    internal class DateWrapperTests
    {
        private IDateWrapper _dateWrapper;

        [SetUp]
        public void SetUp()
            => _dateWrapper = new DateWrapper();

        [Test]
        public void GetCurrentDateTime_CalledTwice_SecondReturnedTimeBiggerThanFirstReturned()
        {
            var firstTime = _dateWrapper.GetCurrentDateTime();
            var secondTime = _dateWrapper.GetCurrentDateTime();

            Assert.That(secondTime, Is.GreaterThan(firstTime));
        }
    }
}
