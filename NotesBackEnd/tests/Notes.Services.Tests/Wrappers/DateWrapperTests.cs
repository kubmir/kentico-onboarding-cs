using System;
using System.Threading;
using Notes.Contracts.Services.Wrappers;
using Notes.Services.Wrappers;
using NUnit.Framework;

namespace Notes.Services.Tests.Wrappers
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
            const Int32 timeout = 100;

            var firstTime = _dateWrapper.GetCurrentDateTime();
            Thread.Sleep(timeout);
            var secondTime = _dateWrapper.GetCurrentDateTime();

            Assert.That(secondTime, Is.GreaterThan(firstTime));
        }
    }
}
