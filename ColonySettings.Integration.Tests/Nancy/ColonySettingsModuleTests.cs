using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using Selkie.Web.MicroServices.ColonySettings.DataAccess;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy;
using Selkie.Web.MicroServices.ColonySettings.Nancy;

namespace Selkie.Web.MicroServices.ColonySettings.Integration.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    [Category("Integration")]
    public sealed class ColonySettingsModuleTests
    {
        [SetUp]
        public void Setup()
        {
            DeleteAllItems();
        }

        private const string BaseUrl = "/colonysettings/";

        private static readonly IColonySettingsRepository m_Repository =
            new ColonySettingsRepository(new ColonySettingsContext());

        private void DeleteAllItems()
        {
            lock ( this )
            {
                foreach ( IColonySettings colony in m_Repository.All )
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

        private void AssertColony(IColonySettingsForResponse expected,
                                  IColonySettingsForResponse actual)
        {
            Console.WriteLine("Comparing colonies with id {0} and {1}...",
                              expected.ColonySettingsId,
                              actual.ColonySettingsId);

            Assert.AreEqual(expected.ColonySettingsId,
                            actual.ColonySettingsId,
                            "ColonySettingsId");

            Assert.AreEqual(expected.ColonyId,
                            actual.ColonyId,
                            "ColonyId");

            Assert.AreEqual(expected.IsFixedStartNode,
                            actual.IsFixedStartNode,
                            "IsFixedStartNode");

            Assert.AreEqual(expected.FixedStartNode,
                            actual.FixedStartNode,
                            "FixedStartNode");

            Assert.AreEqual(expected.CostPerFeature,
                            actual.CostPerFeature,
                            "CostPerFeature");

            Assert.AreEqual(expected.CostMatrix,
                            actual.CostMatrix,
                            "CostMatrix");
        }

        private IColonySettingsForResponse CreateItem(Browser browser)
        {
            IColonySettingsForResponse model = CreateModelForPutTest();

            BrowserResponse result = browser.Post(BaseUrl,
                                                  with =>
                                                  {
                                                      with.JsonBody(model);
                                                  });

            var response = result.Body.DeserializeJson <ColonySettingsForResponse>();

            return response;
        }

        private IColonySettingsForResponse CreateModelForPutTest()
        {
            var model = new ColonySettingsForResponse
                        {
                            CostMatrix = new[]
                                         {
                                             new[]
                                             {
                                                 1,
                                                 2
                                             },
                                             new[]
                                             {
                                                 1,
                                                 2
                                             }
                                         },
                            CostPerFeature = new[]
                                             {
                                                 1,
                                                 2,
                                                 3
                                             },
                            FixedStartNode = 1,
                            IsFixedStartNode = true,
                            ColonyId = 1
                        };

            return model;
        }

        [Test]
        public void Should_return_JSON_string_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonySettingsForResponse expected = CreateItem(browser);

            expected.CostMatrix = new[]
                                  {
                                      new[]
                                      {
                                          1,
                                          2
                                      },
                                      new[]
                                      {
                                          3,
                                          4
                                      }
                                  };
            expected.CostPerFeature = new[]
                                      {
                                          1,
                                          2
                                      };
            expected.FixedStartNode = 2;
            expected.IsFixedStartNode = false;
            expected.ColonyId = 123;

            // When
            BrowserResponse result = browser.Put(BaseUrl,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <ColonySettingsForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonySettingsForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BaseUrl + expected.ColonySettingsId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            var actual = result.Body.DeserializeJson <ColonySettingsForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonySettingsForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BaseUrl + expected.ColonySettingsId,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <ColonySettingsForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_list_requested()
        {
            // Given
            Browser browser = CreateBrowser();

            IColonySettingsForResponse one = CreateItem(browser);
            IColonySettingsForResponse two = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BaseUrl,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <ColonySettingsForResponse[]>();

            // Then
            Assert.AreEqual(2,
                            actual.Length);
        }

        [Test]
        public void Should_return_status_OK_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonySettingsForResponse expected = CreateItem(browser);

            expected.CostMatrix = new[]
                                  {
                                      new[]
                                      {
                                          1,
                                          2
                                      },
                                      new[]
                                      {
                                          3,
                                          4
                                      }
                                  };
            expected.CostPerFeature = new[]
                                      {
                                          1,
                                          2
                                      };
            expected.FixedStartNode = 2;
            expected.IsFixedStartNode = false;
            expected.ColonyId = 123;

            // When
            BrowserResponse result = browser.Put(BaseUrl,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <ColonySettingsForResponse>();

            // Then
            Assert.AreEqual(HttpStatusCode.OK,
                            result.StatusCode);
        }

        [Test]
        public void Should_return_status_OK_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            IColonySettingsForResponse colony = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BaseUrl + colony.ColonySettingsId,
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
            IColonySettingsForResponse colony = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BaseUrl + colony.ColonySettingsId,
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

            IColonySettingsForResponse one = CreateItem(browser);
            IColonySettingsForResponse two = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BaseUrl,
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