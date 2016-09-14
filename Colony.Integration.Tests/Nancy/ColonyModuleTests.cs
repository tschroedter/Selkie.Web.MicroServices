using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using Selkie.Web.MicroServices.Colony.DataAccess;
using Selkie.Web.MicroServices.Colony.Integration.Tests.Nancy;
using Selkie.Web.MicroServices.Colony.Interfaces.DataAccess;
using Selkie.Web.MicroServices.Colony.Interfaces.Nancy;
using Selkie.Web.MicroServices.Colony.Nancy;

namespace Selkie.Web.MicroServices.Colony.Tests.Integration.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    [Category("Integration")]
    public sealed class ColonyModuleTests
    {
        [SetUp]
        public void Setup()
        {
            DeleteAllItems();
        }

        private const string BasePath = "/colonies/";

        private static readonly IColonyRepository m_Repository = new ColonyRepository(new ColonyContext());

        private void DeleteAllItems()
        {
            lock ( this )
            {
                foreach ( IColony colony in m_Repository.All )
                {
                    m_Repository.Remove(colony);
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

        private void AssertColony(IColonyForResponse expected,
                                  IColonyForResponse actual)
        {
            Console.WriteLine("Comparing colonies with id {0} and {1}...",
                              expected.ColonyId,
                              actual.ColonyId);

            Assert.AreEqual(expected.ColonyId,
                            actual.ColonyId,
                            "ColonyId");

            Assert.AreEqual(expected.Description,
                            actual.Description,
                            "Description");

            Assert.AreEqual(expected.Status,
                            actual.Status,
                            "Status");
        }

        private IColonyForResponse CreateItem(Browser browser)
        {
            IColonyForResponse model = CreateModelForPutTest();

            BrowserResponse result = browser.Post(BasePath,
                                                  with =>
                                                  {
                                                      with.JsonBody(model);
                                                  });

            var response = result.Body.DeserializeJson <ColonyForResponse>();

            return response;
        }

        private IColonyForResponse CreateModelForPutTest()
        {
            var model = new ColonyForResponse
                        {
                            ColonyId = Guid.Empty,
                            Description = "PutTest",
                            Status = "Created"
                        };

            return model;
        }

        [Test]
        public void Should_return_JSON_string_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonyForResponse expected = CreateItem(browser);

            expected.Description = "New Description";
            expected.Status = "Finished";

            // When
            BrowserResponse result = browser.Put(BasePath,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <ColonyForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonyForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BasePath + expected.ColonyId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            var actual = result.Body.DeserializeJson <ColonyForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonyForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath + expected.ColonyId,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <ColonyForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_list_requested()
        {
            // Given
            Browser browser = CreateBrowser();

            IColonyForResponse one = CreateItem(browser);
            IColonyForResponse two = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <ColonyForResponse[]>();

            // Then
            Assert.AreEqual(2,
                            actual.Length);
        }

        [Test]
        public void Should_return_status_OK_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonyForResponse expected = CreateItem(browser);

            expected.Description = "New Description";
            expected.Status = "Finished";

            // When
            BrowserResponse result = browser.Put(BasePath,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <ColonyForResponse>();

            // Then
            Assert.AreEqual(HttpStatusCode.OK,
                            result.StatusCode);
        }

        [Test]
        public void Should_return_status_OK_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonyForResponse colony = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BasePath + colony.ColonyId,
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
            IColonyForResponse colony = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath + colony.ColonyId,
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

            IColonyForResponse one = CreateItem(browser);
            IColonyForResponse two = CreateItem(browser);

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