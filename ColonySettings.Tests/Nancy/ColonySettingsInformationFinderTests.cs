using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy;
using Selkie.Web.MicroServices.ColonySettings.Nancy;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;

namespace Selkie.Web.MicroServices.ColonySettings.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ColonySettingsInformationFinderTests
        : InformationFinderBaseTests
              <IColonySettingsForResponse, IColonySettings, IColonySettingsRepository, IColonySettingsInformationFinder>
    {
        protected override IColonySettingsRepository CreateRepository()
        {
            return Substitute.For <IColonySettingsRepository>();
        }

        protected override IColonySettingsInformationFinder CreateSut(IColonySettingsRepository repository)
        {
            return new ColonySettingsInformationFinder(repository);
        }

        protected override IColonySettingsForResponse CreateResponse()
        {
            return new ColonySettingsForResponse
                   {
                       ColonySettingsId = Guid.NewGuid()
                   };
        }

        protected override IColonySettings CreateEntity()
        {
            return new ColonySettings.DataAccess.ColonySettings
                   {
                       ColonyId = Guid.NewGuid()
                   };
        }

        private void AssertResponseAgainstEntity(
            [NotNull] IColonySettingsForResponse response,
            [NotNull] IColonySettings entity)
        {
            Assert.AreEqual(response.ColonySettingsId,
                            entity.ColonySettingsId,
                            "ColonySettingsId");
            Assert.AreEqual(response.CostMatrix,
                            entity.CostMatrix,
                            "CostMatrix");
            Assert.True(response.CostPerFeature.SequenceEqual(entity.CostPerFeature),
                        "CostPerFeature");
            Assert.AreEqual(response.FixedStartNode,
                            entity.FixedStartNode,
                            "FixedStartNode");
            Assert.AreEqual(response.IsFixedStartNode,
                            entity.IsFixedStartNode,
                            "IsFixedStartNode");
            Assert.AreEqual(response.ColonyId,
                            entity.ColonyId,
                            "ColonyId");
            Assert.AreEqual(response.ColonySettingsId,
                            entity.ColonySettingsId,
                            "ColonySettingsId");
        }

        private void AssertEntityAgainstResponse(
            [NotNull] IColonySettings entity,
            [NotNull] IColonySettingsForResponse response)
        {
            AssertResponseAgainstEntity(response,
                                        entity);
        }

        [Theory]
        [AutoNSubstituteData]
        public void ToEntity_ReturnsEntity_WhenCalled(
            [NotNull] ColonySettingsForResponse response)
        {
            // Arrange
            // Act
            IColonySettings actual = ColonySettingsInformationFinder.ToEntity(response);

            // Assert
            AssertResponseAgainstEntity(response,
                                        actual);
        }

        [Theory]
        [AutoNSubstituteData]
        public void ToResponse_ReturnsEntityForResponse_WhenCalled(
            [NotNull] ColonySettings.DataAccess.ColonySettings entity)
        {
            // Arrange
            // Act
            ColonySettingsForResponse actual = ColonySettingsInformationFinder.ToResponse(entity);

            // Assert
            AssertEntityAgainstResponse(entity,
                                        actual);
        }

        [Theory]
        [AutoNSubstituteData]
        public void ToResponses_ReturnsResponses_ForEntities(
            [NotNull] ColonySettings.DataAccess.ColonySettings one,
            [NotNull] ColonySettings.DataAccess.ColonySettings two)
        {
            // Arrange
            var entities = new[]
                           {
                               one,
                               two
                           };

            // Act
            IEnumerable <ColonySettingsForResponse> actual = ColonySettingsInformationFinder.ToResponses(entities);

            // Assert
            Assert.AreEqual(2,
                            actual.Count());
        }
    }
}