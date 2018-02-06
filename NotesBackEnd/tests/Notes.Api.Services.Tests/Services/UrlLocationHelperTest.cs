using System;
using System.Web.Http.Routing;
using Notes.Api.Services.Services;
using Notes.Contracts.ApiServices;
using NSubstitute;
using NUnit.Framework;

namespace Notes.Api.Services.Tests.Services
{
    [TestFixture]
    internal class UrlLocationHelperTest
    {
        private const string Id = "2c00d1c2-fd2b-4c06-8f2d-130e88f719c2";

        private IUrlLocationHelper _urlLocationHelper;
        private UrlHelper _urlHelper;
        private IRouteManager _routeManager;

        [SetUp]
        public void Init()
        {
            _urlHelper = Substitute.For<UrlHelper>();
            _routeManager = Substitute.For<IRouteManager>();

            _urlHelper.Route(Arg.Any<string>(), Arg.Any<object>()).Returns($"/{Id}/test");
            _routeManager.GetNotesRouteName().Returns("test");

            _urlLocationHelper = new UrlLocationHelper(_urlHelper, _routeManager);
        }

        [Test]
        public void GetUrlWithId_ReturnsCorrectLocation()
        {
            var actualUrl = _urlLocationHelper.GetNotesUrlWithId(new Guid(Id));

            Assert.That(actualUrl, Is.EqualTo($"/{Id}/test"));
        }
    }
}
