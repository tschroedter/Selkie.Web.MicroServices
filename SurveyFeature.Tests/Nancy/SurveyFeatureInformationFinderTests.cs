using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.DataAccess;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.Nancy;
using Selkie.Web.MicroServices.SurveyFeature.Nancy;

namespace Selkie.Web.MicroServices.Nancy.Colony.Tests
{
    internal sealed class SurveyFeatureInformationFinderTests
        : InformationFinderBaseTests
              <ISurveyFeatureForResponse, ISurveyFeature, ISurveyFeatureRepository, ISurveyFeatureInformationFinder>
    {
        private static int NextIdForResponse;
        private static int NextIdForEntity;

        protected override ISurveyFeatureRepository CreateRepository()
        {
            return Substitute.For <ISurveyFeatureRepository>();
        }

        protected override ISurveyFeatureInformationFinder CreateSut(ISurveyFeatureRepository repository)
        {
            return new SurveyFeatureInformationFinder(repository);
        }

        protected override ISurveyFeatureForResponse CreateResponse()
        {
            return new SurveyFeatureForResponse
                   {
                       SurveyFeatureId = NextIdForResponse++
                   };
        }

        protected override ISurveyFeature CreateEntity()
        {
            return new SurveyFeature.DataAccess.SurveyFeature
                   {
                       SurveyFeatureId = NextIdForEntity++
                   };
        }

        private void AssertResponseAgainstEntity(
            [NotNull] ISurveyFeatureForResponse response,
            [NotNull] ISurveyFeature entity)
        {
            NUnitHelper.AssertIsEquivalent(response.AngleToXAxisAtEndPoint,
                                           entity.AngleToXAxisAtEndPoint,
                                           "AngleToXAxisAtEndPoint");
            NUnitHelper.AssertIsEquivalent(response.AngleToXAxisAtStartPoint,
                                           entity.AngleToXAxisAtStartPoint,
                                           "AngleToXAxisAtStartPoint");
            Assert.AreEqual(response.ColonyId,
                            entity.ColonyId,
                            "ColonyId");
            NUnitHelper.AssertIsEquivalent(response.EndPointX,
                                           entity.EndPointX,
                                           "EndPointX");
            NUnitHelper.AssertIsEquivalent(response.EndPointY,
                                           entity.EndPointY,
                                           "EndPointY");
            Assert.AreEqual(response.IsUnknown,
                            entity.IsUnknown,
                            "IsUnknown");
            NUnitHelper.AssertIsEquivalent(response.Length,
                                           entity.Length,
                                           "Length");
            Assert.AreEqual(response.RunDirection,
                            entity.RunDirection,
                            "RunDirection");
            NUnitHelper.AssertIsEquivalent(response.StartPointX,
                                           entity.StartPointX,
                                           "StartPointX");
            NUnitHelper.AssertIsEquivalent(response.StartPointY,
                                           entity.StartPointY,
                                           "StartPointY");
            Assert.AreEqual(response.SurveyFeatureId,
                            entity.SurveyFeatureId,
                            "SurveyFeatureId");
        }

        private void AssertEntityAgainstResponse(
            [NotNull] ISurveyFeature entity,
            [NotNull] ISurveyFeatureForResponse response)
        {
            AssertResponseAgainstEntity(response,
                                        entity);
        }

        [Test]
        public void ToEntity_ReturnsEntity_WhenCalled()
        {
            // Arrange
            ISurveyFeatureForResponse response = CreateResponse();

            // Act
            ISurveyFeature actual = SurveyFeatureInformationFinder.ToEntity(response);

            // Assert
            AssertResponseAgainstEntity(response,
                                        actual);
        }

        [Test]
        public void ToEntityForResponse_ReturnsEntityForResponse_WhenCalled()
        {
            // Arrange
            ISurveyFeature entity = CreateEntity();

            // Act
            SurveyFeatureForResponse actual = SurveyFeatureInformationFinder.ToEntityForResponse(entity);

            // Assert
            AssertEntityAgainstResponse(entity,
                                        actual);
        }

        [Test]
        public void ToResponses_ReturnsResponses_ForEntities()
        {
            // Arrange
            ISurveyFeature one = CreateEntity();
            ISurveyFeature two = CreateEntity();
            var entities = new[]
                           {
                               one,
                               two
                           };

            // Act
            IEnumerable <SurveyFeatureForResponse> actual = SurveyFeatureInformationFinder.ToResponses(entities);

            // Assert
            Assert.AreEqual(2,
                            actual.Count());
        }
    }
}