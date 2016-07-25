using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.BestTrail.Interfaces.DataAccess;
using Selkie.Web.MicroServices.BestTrail.Interfaces.Nancy;
using Selkie.Web.MicroServices.BestTrail.Nancy;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;

namespace Selkie.Web.MicroServices.Nancy.Colony.Tests
{
    internal sealed class BestTrailInformationFinderTests
        : InformationFinderBaseTests
              <IBestTrailForResponse, IBestTrail, IBestTrailRepository, IBestTrailInformationFinder>
    {
        private static int NextIdForResponse;
        private static int NextIdForEntity;

        protected override IBestTrailRepository CreateRepository()
        {
            return Substitute.For <IBestTrailRepository>();
        }

        protected override IBestTrailInformationFinder CreateSut(IBestTrailRepository repository)
        {
            return new BestTrailInformationFinder(repository);
        }

        protected override IBestTrailForResponse CreateResponse()
        {
            return new BestTrailForResponse
                   {
                       BestTrailId = NextIdForResponse++
                   };
        }

        protected override IBestTrail CreateEntity()
        {
            return new BestTrail.DataAccess.BestTrail
                   {
                       BestTrailId = NextIdForEntity++
                   };
        }

        private void AssertResponseAgainstEntity(
            [NotNull] IBestTrailForResponse response,
            [NotNull] IBestTrail entity)
        {
            NUnitHelper.AssertIsEquivalent(response.Alpha,
                                           entity.Alpha,
                                           "Alpha");
            NUnitHelper.AssertIsEquivalent(response.BestTrailId,
                                           entity.BestTrailId,
                                           "BestTrailId");
            NUnitHelper.AssertIsEquivalent(response.Beta,
                                           entity.Beta,
                                           "Beta");
            NUnitHelper.AssertIsEquivalent(response.ColonyId,
                                           entity.ColonyId,
                                           "ColonyId");
            NUnitHelper.AssertIsEquivalent(response.Gamma,
                                           entity.Gamma,
                                           "Gamma");
            NUnitHelper.AssertIsEquivalent(response.Iteration,
                                           entity.Iteration,
                                           "Iteration");
            NUnitHelper.AssertIsEquivalent(response.Length,
                                           entity.Length,
                                           "Length");
            Assert.True(response.Trail.SequenceEqual(entity.Trail),
                        "Trail");
            Assert.AreEqual(response.Type,
                            entity.Type,
                            "Type");
        }

        private void AssertEntityAgainstResponse(
            [NotNull] IBestTrail entity,
            [NotNull] IBestTrailForResponse response)
        {
            AssertResponseAgainstEntity(response,
                                        entity);
        }

        [Test]
        public void ToEntity_ReturnsEntity_WhenCalled()
        {
            // Arrange
            IBestTrailForResponse response = CreateResponse();

            // Act
            IBestTrail actual = BestTrailInformationFinder.ToEntity(response);

            // Assert
            AssertResponseAgainstEntity(response,
                                        actual);
        }

        [Test]
        public void ToEntityForResponse_ReturnsEntityForResponse_WhenCalled()
        {
            // Arrange
            IBestTrail entity = CreateEntity();

            // Act
            BestTrailForResponse actual = BestTrailInformationFinder.ToEntityForResponse(entity);

            // Assert
            AssertEntityAgainstResponse(entity,
                                        actual);
        }

        [Test]
        public void ToResponses_ReturnsResponses_ForEntities()
        {
            // Arrange
            IBestTrail one = CreateEntity();
            IBestTrail two = CreateEntity();
            var entities = new[]
                           {
                               one,
                               two
                           };

            // Act
            IEnumerable <BestTrailForResponse> actual = BestTrailInformationFinder.ToResponses(entities);

            // Assert
            Assert.AreEqual(2,
                            actual.Count());
        }
    }
}