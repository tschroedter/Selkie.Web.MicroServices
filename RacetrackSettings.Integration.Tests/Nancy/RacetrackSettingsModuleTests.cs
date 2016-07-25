using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using Selkie.Web.MicroServices.RacetrackSettings.DataAccess;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.DataAccess;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.Nancy;
using Selkie.Web.MicroServices.RacetrackSettings.Nancy;

namespace Selkie.Web.MicroServices.RacetrackSettings.Integration.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    [Category("Integration")]
    public sealed class RacetrackSettingsModuleTests
    {
        [SetUp]
        public void Setup()
        {
            DeleteAllItems();
        }

        private const string BasePath = "/racetracksettings/";

        private static readonly IRacetrackSettingsRepository m_Repository =
            new RacetrackSettingsRepository(new RacetrackSettingsContext());

        private void DeleteAllItems()
        {
            lock ( this )
            {
                IRacetrackSettings[] bestTrails = m_Repository.All.ToArray();

                foreach ( IRacetrackSettings bestTrail in bestTrails )
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

        private void AssertColony(IRacetrackSettingsForResponse expected,
                                  IRacetrackSettingsForResponse actual)
        {
            Console.WriteLine("Comparing RacetrackSettingss with id {0} and {1}...",
                              expected.RacetrackSettingsId,
                              actual.RacetrackSettingsId);

            Assert.AreEqual(expected.RacetrackSettingsId,
                            actual.RacetrackSettingsId,
                            "RacetrackSettingsId");

            Assert.AreEqual(expected.ColonyId,
                            actual.ColonyId,
                            "ColonyId");

            Assert.AreEqual(expected.IsPortTurnAllowed,
                            actual.IsPortTurnAllowed,
                            "IsPortTurnAllowed");

            Assert.AreEqual(expected.IsStarboardTurnAllowed,
                            actual.IsStarboardTurnAllowed,
                            "IsStarboardTurnAllowed");

            Assert.AreEqual(expected.TurnRadiusForPort,
                            actual.TurnRadiusForPort,
                            "TurnRadiusForPort");

            Assert.AreEqual(expected.TurnRadiusForStarboard,
                            actual.TurnRadiusForStarboard,
                            "TurnRadiusForStarboard");
        }

        private IRacetrackSettingsForResponse CreateItem(Browser browser)
        {
            IRacetrackSettingsForResponse model = CreateModelForPutTest();

            BrowserResponse result = browser.Post(BasePath,
                                                  with =>
                                                  {
                                                      with.JsonBody(model);
                                                  });

            var response = result.Body.DeserializeJson <RacetrackSettingsForResponse>();

            return response;
        }

        private IRacetrackSettingsForResponse CreateModelForPutTest()
        {
            var model = new RacetrackSettingsForResponse
                        {
                            ColonyId = 1,
                            IsPortTurnAllowed = true,
                            IsStarboardTurnAllowed = false,
                            TurnRadiusForPort = 123.0,
                            TurnRadiusForStarboard = 456.0
                        };

            return model;
        }

        [Test]
        public void Should_return_JSON_string_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            IRacetrackSettingsForResponse expected = CreateItem(browser);

            expected.ColonyId = 11;
            expected.IsPortTurnAllowed = false;
            expected.IsStarboardTurnAllowed = true;
            expected.TurnRadiusForPort = 1234.0;
            expected.TurnRadiusForStarboard = 4567.0;

            // When
            BrowserResponse result = browser.Put(BasePath,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <RacetrackSettingsForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            IRacetrackSettingsForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BasePath + expected.RacetrackSettingsId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            var actual = result.Body.DeserializeJson <RacetrackSettingsForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();
            IRacetrackSettingsForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath + expected.RacetrackSettingsId,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <RacetrackSettingsForResponse>();

            // Then
            AssertColony(expected,
                         actual);
        }

        [Test]
        public void Should_return_JSON_string_when_list_requested()
        {
            // Given
            Browser browser = CreateBrowser();

            IRacetrackSettingsForResponse one = CreateItem(browser);
            IRacetrackSettingsForResponse two = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <RacetrackSettingsForResponse[]>();

            // Then
            Assert.AreEqual(2,
                            actual.Length);
        }

        [Test]
        public void Should_return_status_OK_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            IRacetrackSettingsForResponse expected = CreateItem(browser);

            expected.ColonyId = 1;
            expected.IsPortTurnAllowed = true;
            expected.IsStarboardTurnAllowed = false;
            expected.TurnRadiusForPort = 123.0;
            expected.TurnRadiusForStarboard = 456.0;

            // When
            BrowserResponse result = browser.Put(BasePath,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <RacetrackSettingsForResponse>();

            // Then
            Assert.AreEqual(HttpStatusCode.OK,
                            result.StatusCode);
        }

        [Test]
        public void Should_return_status_OK_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            IRacetrackSettingsForResponse bestTrail = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BasePath + bestTrail.RacetrackSettingsId,
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
            IRacetrackSettingsForResponse bestTrail = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath + bestTrail.RacetrackSettingsId,
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

            IRacetrackSettingsForResponse one = CreateItem(browser);
            IRacetrackSettingsForResponse two = CreateItem(browser);

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