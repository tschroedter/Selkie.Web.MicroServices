using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using Selkie.Web.MicroServices.BestTrail.DataAccess;
using Selkie.Web.MicroServices.BestTrail.Interfaces.DataAccess;
using Selkie.Web.MicroServices.BestTrail.Interfaces.Nancy;
using Selkie.Web.MicroServices.BestTrail.Nancy;

namespace Selkie.Web.MicroServices.BestTrail.Integration.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    [Category("Integration")]
    public sealed class BestTrailModuleTests
    {
        [SetUp]
        public void Setup()
        {
            DeleteAllItems();
        }

        private const string BasePath = "/besttrail/";

        private static readonly IBestTrailRepository m_Repository = new BestTrailRepository(new BestTrailContext());

        private void DeleteAllItems()
        {
            lock ( this )
            {
                IBestTrail[] bestTrails = m_Repository.All.ToArray();

                foreach ( IBestTrail bestTrail in bestTrails )
                {
                    m_Repository.Remove(bestTrail);
                }
            }
        }

        private static Browser CreateBrowser()
        {
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper,
                                      to => to.Accept("application/json"));
            return browser;
        }

        private void AssertColony(IBestTrailForResponse expected,
                                  IBestTrailForResponse actual)
        {
            Console.WriteLine("Comparing BestTrails with id {0} and {1}...",
                              expected.BestTrailId,
                              actual.BestTrailId);

            Assert.AreEqual(expected.BestTrailId,
                            actual.BestTrailId,
                            "BestTrailId");

            Assert.AreEqual(expected.ColonyId,
                            actual.ColonyId,
                            "ColonyId");

            Assert.AreEqual(expected.Alpha,
                            actual.Alpha,
                            "Alpha");

            Assert.AreEqual(expected.Beta,
                            actual.Beta,
                            "Beta");

            Assert.AreEqual(expected.Gamma,
                            actual.Gamma,
                            "Gamma");

            Assert.AreEqual(expected.Iteration,
                            actual.Iteration,
                            "Iteration");

            Assert.AreEqual(expected.Length,
                            actual.Length,
                            "Length");

            Assert.True(expected.Trail.ToArray().SequenceEqual(actual.Trail),
                        "Trail");

            Assert.AreEqual(expected.Type,
                            actual.Type,
                            "Type");
        }

        private IBestTrailForResponse CreateItem(Browser browser)
        {
            IBestTrailForResponse model = CreateModelForPutTest();

            BrowserResponse result = browser.Post(BasePath,
                                                  with =>
                                                  {
                                                      with.JsonBody(model);
                                                  });

            var response = result.Body.DeserializeJson <BestTrailForResponse>();

            return response;
        }

        private IBestTrailForResponse CreateModelForPutTest()
        {
            var model = new BestTrailForResponse
                        {
                            Alpha = 1.0,
                            Beta = 2.0,
                            ColonyId = 3,
                            Gamma = 4.0,
                            Iteration = 5,
                            Length = 6,
                            Trail = new[]
                                    {
                                        7,
                                        8
                                    },
                            Type = "Unknown"
                        };

            return model;
        }

        [Test]
        public void Should_return_JSON_string_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            IBestTrailForResponse expected = CreateItem(browser);

            expected.Alpha = 11.0;
            expected.Beta = 22.0;
            expected.ColonyId = 3;
            expected.Gamma = 44.0;
            expected.Iteration = 5;
            expected.Length = 6;
            expected.Trail = new[]
                             {
                                 77,
                                 88
                             };
            expected.Type = "UnknownUnknown";

            // When
            BrowserResponse result = browser.Put(BasePath,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <BestTrailForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            IBestTrailForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BasePath + expected.BestTrailId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            var actual = result.Body.DeserializeJson <BestTrailForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();
            IBestTrailForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath + expected.BestTrailId,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <BestTrailForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_list_requested()
        {
            // Given
            Browser browser = CreateBrowser();

            IBestTrailForResponse one = CreateItem(browser);
            IBestTrailForResponse two = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <BestTrailForResponse[]>();

            // Then
            Assert.AreEqual(2,
                            actual.Length);
        }

        [Test]
        public void Should_return_status_OK_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            IBestTrailForResponse expected = CreateItem(browser);

            expected.Alpha = 1.0;
            expected.Beta = 3.0;
            expected.ColonyId = 4;
            expected.Gamma = 5.0;
            expected.Iteration = 6;
            expected.Length = 7;
            expected.Trail = new[]
                             {
                                 1,
                                 2
                             };
            expected.Type = "Unknown";

            // When
            BrowserResponse result = browser.Put(BasePath,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <BestTrailForResponse>();

            // Then
            Assert.AreEqual(HttpStatusCode.OK,
                            result.StatusCode);
        }

        [Test]
        public void Should_return_status_OK_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            IBestTrailForResponse bestTrail = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BasePath + bestTrail.BestTrailId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            // Then
            Assert.AreEqual(HttpStatusCode.OK,
                            result.StatusCode);
        }

        [Test]
        public void Should_return_status_OK_when_item_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();
            IBestTrailForResponse bestTrail = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath + bestTrail.BestTrailId,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.AreEqual(HttpStatusCode.OK,
                            result.StatusCode);
        }

        [Test]
        public void Should_return_status_OK_when_list_requested()
        {
            // Given
            Browser browser = CreateBrowser();

            IBestTrailForResponse one = CreateItem(browser);
            IBestTrailForResponse two = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            // Then
            Assert.AreEqual(HttpStatusCode.OK,
                            result.StatusCode);
        }
    }
}