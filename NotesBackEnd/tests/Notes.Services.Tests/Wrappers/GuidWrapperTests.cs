using System;
using System.Collections.Generic;
using System.Linq;
using Notes.Contracts.Services.Wrappers;
using Notes.Services.Wrappers;
using NUnit.Framework;

namespace Notes.Services.Tests.Wrappers
{
    internal class GuidWrapperTests
    {
        private IGuidWrapper _guidWrapper;

        [SetUp]
        public void SetUp()
            => _guidWrapper = new GuidWrapper();
        
        [Test]
        public void GenerateGuid_GenerateNewGuid_NonEmptyGuidReturned()
        {
            var generatedGuid = _guidWrapper.GetNew();

            Assert.That(generatedGuid, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GenerateGuid_Generate100Guids_UniqueGuidsReturned()
        {
            List<Guid> generatedGuids = new List<Guid>();

            for (int i = 0; i < 100; i++)
            {
                generatedGuids.Add(_guidWrapper.GetNew());
            }

            Assert.That(generatedGuids.Distinct().Count(), Is.EqualTo(generatedGuids.Count));
        }
    }
}
