using System;
using System.Net.Http;
using System.Web.Http;
using Notes.Api.Helpers;
using Notes.Contracts.ApiHelpers;
using NSubstitute;
using NUnit.Framework;

namespace Notes.Api.Tests.Helpers
{
    [TestFixture]
    internal class UrlLocationHelperTest
    {
        private IUrlLocationHelper _urlLocationHelper;
        private HttpRequestMessage _requestMessage;

        [SetUp]
        public void Init()
        {
            _requestMessage = Substitute.For<HttpRequestMessage>();
            ConfigureRequestMessage();
            _urlLocationHelper = new UrlLocationHelper(_requestMessage);
        }

        [Test]
        public void GetUrlWithId_ReturnsCorrectLocation()
        {
            var actualUrl = _urlLocationHelper.GetUrlWithId("test", new Guid("2c00d1c2-fd2b-4c06-8f2d-130e88f719c2"));

            Assert.That(actualUrl, Is.EqualTo("/2c00d1c2-fd2b-4c06-8f2d-130e88f719c2/test"));
        }

        private void ConfigureRequestMessage()
        {
            _requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri("http://test")
            };

            var configuration = new HttpConfiguration();
            configuration.Routes.MapHttpRoute(
                name: "test",
                routeTemplate: "{id}/test"
            );

            _requestMessage.SetConfiguration(configuration);
        }
    }
}
