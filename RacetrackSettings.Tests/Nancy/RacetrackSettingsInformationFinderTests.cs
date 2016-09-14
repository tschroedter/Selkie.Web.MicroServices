using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.DataAccess;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.Nancy;
using Selkie.Web.MicroServices.RacetrackSettings.Nancy;

namespace Selkie.Web.MicroServices.RacetrackSettings.Tests.Nancy
{
    internal sealed class RacetrackSettingsInformationFinderTests
        : InformationFinderBaseTests
              <IRacetrackSettingsForResponse, IRacetrackSettings, IRacetrackSettingsRepository,
              IRacetrackSettingsInformationFinder>
    {
        protected override IRacetrackSettingsRepository CreateRepository()
        {
            return Substitute.For <IRacetrackSettingsRepository>();
        }

        protected override IRacetrackSettingsInformationFinder CreateSut(IRacetrackSettingsRepository repository)
        {
            return new RacetrackSettingsInformationFinder(repository);
        }

        protected override IRacetrackSettingsForResponse CreateResponse()
        {
            return new RacetrackSettingsForResponse
                   {
                       RacetrackSettingsId = Guid.NewGuid()
                   };
        }

        protected override IRacetrackSettings CreateEntity()
        {
            return new DataAccess.RacetrackSettings
                   {
                       RacetrackSettingsId = Guid.NewGuid()
                   };
        }

        private void AssertResponseAgainstEntity(
            [NotNull] IRacetrackSettingsForResponse response,
            [NotNull] IRacetrackSettings entity)
        {
            Assert.AreEqual(response.RacetrackSettingsId,
                            entity.RacetrackSettingsId,
                            "RacetrackSettingsId");
            Assert.AreEqual(response.ColonyId,
                            entity.ColonyId,
                            "ColonyId");
            Assert.AreEqual(response.IsPortTurnAllowed,
                            entity.IsPortTurnAllowed,
                            "IsPortTurnAllowed");
            Assert.AreEqual(response.IsStarboardTurnAllowed,
                            entity.IsStarboardTurnAllowed,
                            "IsStarboardTurnAllowed");
            NUnitHelper.AssertIsEquivalent(response.TurnRadiusForPort,
                                           entity.TurnRadiusForPort,
                                           "TurnRadiusForPort");
            NUnitHelper.AssertIsEquivalent(response.TurnRadiusForStarboard,
                                           entity.TurnRadiusForStarboard,
                                           "TurnRadiusForStarboard");
        }

        private void AssertEntityAgainstResponse(
            [NotNull] IRacetrackSettings entity,
            [NotNull] IRacetrackSettingsForResponse response)
        {
            AssertResponseAgainstEntity(response,
                                        entity);
        }

        [Test]
        public void ToEntity_ReturnsEntity_WhenCalled()
        {
            // Arrange
            IRacetrackSettingsForResponse response = CreateResponse();

            // Act
            IRacetrackSettings actual = RacetrackSettingsInformationFinder.ToEntity(response);

            // Assert
            AssertResponseAgainstEntity(response,
                                        actual);
        }

        [Test]
        public void ToEntityForResponse_ReturnsEntityForResponse_WhenCalled()
        {
            // Arrange
            IRacetrackSettings entity = CreateEntity();

            // Act
            RacetrackSettingsForResponse actual = RacetrackSettingsInformationFinder.ToResponse(entity);

            // Assert
            AssertEntityAgainstResponse(entity,
                                        actual);
        }

        [Test]
        public void ToResponses_ReturnsResponses_ForEntities()
        {
            // Arrange
            IRacetrackSettings one = CreateEntity();
            IRacetrackSettings two = CreateEntity();
            var entities = new[]
                           {
                               one,
                               two
                           };

            // Act
            IEnumerable <RacetrackSettingsForResponse> actual = RacetrackSettingsInformationFinder.ToResponses(entities);

            // Assert
            Assert.AreEqual(2,
                            actual.Count());
        }
    }
}