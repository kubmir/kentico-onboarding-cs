using System;
using System.Collections.Generic;
using System.Linq;
using Notes.Contracts.Services.Utils;
using Notes.Services.StaticWrappers;
using NUnit.Framework;

namespace Notes.Services.Tests.StaticWrappers
{
    internal class GuidServiceTests
    {
        private IGuidService _guidService;

        [SetUp]
        public void SetUp()
            => _guidService = new GuidService();
        
        [Test]
        public void GenerateGuid_GenerateNewGuid_NonEmptyGuidReturned()
        {
            var generatedGuid = _guidService.GetNew();

            Assert.That(generatedGuid, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GenerateGuid_Generate100Guids_UniqueGuidsReturned()
        {
            List<Guid> generatedGuids = new List<Guid>();

            for (int i = 0; i < 100; i++)
            {
                generatedGuids.Add(_guidService.GetNew());
            }

            Assert.That(generatedGuids.Distinct().Count(), Is.EqualTo(generatedGuids.Count));
        }
    }
}
