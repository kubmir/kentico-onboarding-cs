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
        private const String Id = "2c00d1c2-fd2b-4c06-8f2d-130e88f719c2";

        private IUrlLocationHelper _urlLocationHelper;
        private UrlHelper _urlHelper;
        private IRouteOptions _routeOptions;

        [SetUp]
        public void Init()
        {
            _urlHelper = Substitute.For<UrlHelper>();
            _routeOptions = Substitute.For<IRouteOptions>();

            _urlHelper
                .Route("test", Arg.Is<Object>(value => (Guid) new HttpRouteValueDictionary(value)["id"] == new Guid(Id)))
                .Returns($"/{Id}/test");

            _routeOptions
                .GetNotesRouteName()
                .Returns("test");

            _urlLocationHelper = new UrlLocationHelper(_urlHelper, _routeOptions);
        }

        [Test]
        public void GetUrlWithId_BuildUrl_ReturnsCorrectLocation()
        {
            var actualUrl = _urlLocationHelper.GetNotesUrlWithId(new Guid(Id));

            Assert.That(actualUrl, Is.EqualTo($"/{Id}/test"));
        }
    }
}
