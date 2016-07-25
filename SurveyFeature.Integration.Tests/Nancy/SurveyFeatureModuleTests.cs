using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.SurveyFeature.DataAccess;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.DataAccess;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.Nancy;
using Selkie.Web.MicroServices.SurveyFeature.Nancy;

namespace Selkie.Web.MicroServices.SurveyFeature.Integration.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    [Category("Integration")]
    public sealed class SurveyFeatureModuleTests
    {
        [SetUp]
        public void Setup()
        {
            DeleteAllItems();
        }

        private const string BasePath = "/surveyfeature/";

        private static readonly ISurveyFeatureRepository m_Repository =
            new SurveyFeatureRepository(new SurveyFeatureContext());

        private void DeleteAllItems()
        {
            lock ( this )
            {
                ISurveyFeature[] bestTrails = m_Repository.All.ToArray();

                foreach ( ISurveyFeature bestTrail in bestTrails )
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

        private void AssertSurveyFeature(ISurveyFeatureForResponse expected,
                                         ISurveyFeatureForResponse actual)
        {
            Console.WriteLine("Comparing SurveyFeatures with id {0} and {1}...",
                              expected.SurveyFeatureId,
                              actual.SurveyFeatureId);

            NUnitHelper.AssertIsEquivalent(actual.AngleToXAxisAtEndPoint,
                                           expected.AngleToXAxisAtEndPoint,
                                           "AngleToXAxisAtEndPoint");
            NUnitHelper.AssertIsEquivalent(actual.AngleToXAxisAtStartPoint,
                                           expected.AngleToXAxisAtStartPoint,
                                           "AngleToXAxisAtStartPoint");
            Assert.AreEqual(actual.ColonyId,
                            expected.ColonyId,
                            "ColonyId");
            NUnitHelper.AssertIsEquivalent(actual.EndPointX,
                                           expected.EndPointX,
                                           "EndPointX");
            NUnitHelper.AssertIsEquivalent(actual.EndPointY,
                                           expected.EndPointY,
                                           "EndPointY");
            Assert.AreEqual(actual.IsUnknown,
                            expected.IsUnknown,
                            "IsUnknown");
            NUnitHelper.AssertIsEquivalent(actual.Length,
                                           expected.Length,
                                           "Length");
            Assert.AreEqual(actual.RunDirection,
                            expected.RunDirection,
                            "RunDirection");
            NUnitHelper.AssertIsEquivalent(actual.StartPointX,
                                           expected.StartPointX,
                                           "StartPointX");
            NUnitHelper.AssertIsEquivalent(actual.StartPointY,
                                           expected.StartPointY,
                                           "StartPointY");
            Assert.AreEqual(actual.SurveyFeatureId,
                            expected.SurveyFeatureId,
                            "SurveyFeatureId");
        }

        private ISurveyFeatureForResponse CreateItem(Browser browser)
        {
            ISurveyFeatureForResponse model = CreateModelForPutTest();

            BrowserResponse result = browser.Post(BasePath,
                                                  with =>
                                                  {
                                                      with.JsonBody(model);
                                                  });

            var response = result.Body.DeserializeJson <SurveyFeatureForResponse>();

            return response;
        }

        private ISurveyFeatureForResponse CreateModelForPutTest()
        {
            var model = new SurveyFeatureForResponse
                        {
                            AngleToXAxisAtEndPoint = 1.0,
                            AngleToXAxisAtStartPoint = 2.0,
                            ColonyId = 3,
                            EndPointX = 4.0,
                            EndPointY = 5,
                            IsUnknown = false,
                            Length = 6.0,
                            RunDirection = "Forward",
                            StartPointX = 7.0,
                            StartPointY = 8.0
                        };

            return model;
        }

        [Test]
        public void Should_return_JSON_string_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            ISurveyFeatureForResponse expected = CreateItem(browser);

            expected.AngleToXAxisAtEndPoint = 11.0;
            expected.AngleToXAxisAtStartPoint = 22.0;
            expected.ColonyId = 33;
            expected.EndPointX = 44.0;
            expected.EndPointY = 55;
            expected.IsUnknown = false;
            expected.Length = 66.0;
            expected.RunDirection = "Reverse";
            expected.StartPointX = 77.0;
            expected.StartPointY = 88.0;

            // When
            BrowserResponse result = browser.Put(BasePath,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <SurveyFeatureForResponse>();

            // Then
            AssertSurveyFeature(expected,
                                actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            ISurveyFeatureForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BasePath + expected.SurveyFeatureId,
                                                    with =>
                                                    {
                                                        with.HttpRequest();
                                                    });

            var actual = result.Body.DeserializeJson <SurveyFeatureForResponse>();

            // Then
            AssertSurveyFeature(expected,
                                actual);
        }

        [Test]
        public void Should_return_JSON_string_when_item_with_id_exists()
        {
            // Given
            Browser browser = CreateBrowser();
            ISurveyFeatureForResponse expected = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath + expected.SurveyFeatureId,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <SurveyFeatureForResponse>();

            // Then
            AssertSurveyFeature(expected,
                                actual);
        }

        [Test]
        public void Should_return_JSON_string_when_list_requested()
        {
            // Given
            Browser browser = CreateBrowser();

            ISurveyFeatureForResponse one = CreateItem(browser);
            ISurveyFeatureForResponse two = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath,
                                                 with =>
                                                 {
                                                     with.HttpRequest();
                                                 });

            var actual = result.Body.DeserializeJson <SurveyFeatureForResponse[]>();

            // Then
            Assert.AreEqual(2,
                            actual.Length);
        }

        [Test]
        public void Should_return_status_OK_when_existing_item_is_updated()
        {
            // Given
            Browser browser = CreateBrowser();
            ISurveyFeatureForResponse expected = CreateItem(browser);

            expected.AngleToXAxisAtEndPoint = 1.0;
            expected.AngleToXAxisAtStartPoint = 2.0;
            expected.ColonyId = 3;
            expected.EndPointX = 4.0;
            expected.EndPointY = 5;
            expected.IsUnknown = true;
            expected.Length = 6.0;
            expected.RunDirection = "Forward";
            expected.StartPointX = 7.0;
            expected.StartPointY = 8.0;

            // When
            BrowserResponse result = browser.Put(BasePath,
                                                 with =>
                                                 {
                                                     with.JsonBody(expected);
                                                 });

            var actual = result.Body.DeserializeJson <SurveyFeatureForResponse>();

            // Then
            Assert.AreEqual(HttpStatusCode.OK,
                            result.StatusCode);
        }

        [Test]
        public void Should_return_status_OK_when_item_is_deleted()
        {
            // Given
            Browser browser = CreateBrowser();
            ISurveyFeatureForResponse bestTrail = CreateItem(browser);

            // When
            BrowserResponse result = browser.Delete(BasePath + bestTrail.SurveyFeatureId,
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
            ISurveyFeatureForResponse bestTrail = CreateItem(browser);

            // When
            BrowserResponse result = browser.Get(BasePath + bestTrail.SurveyFeatureId,
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

            ISurveyFeatureForResponse one = CreateItem(browser);
            ISurveyFeatureForResponse two = CreateItem(browser);

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