using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.Colony.DataAccess;
using Selkie.Web.MicroServices.Colony.Interfaces.DataAccess;
using Selkie.Web.MicroServices.Colony.Interfaces.Nancy;
using Selkie.Web.MicroServices.Colony.Nancy;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.Nancy.Colony.Tests
{
    internal sealed class ColonyInformationFinderTests
        : InformationFinderBaseTests
              <IColonyForResponse, IColony, IColonyRepository, IColonyInformationFinder>
    {
        protected override IColonyRepository CreateRepository()
        {
            return Substitute.For <IColonyRepository>();
        }

        protected override IColonyInformationFinder CreateSut([NotNull] IColonyRepository repository)
        {
            return new ColonyInformationFinder(Substitute.For <ISelkieLogger>(),
                                               repository);
        }

        protected override IColonyForResponse CreateResponse()
        {
            return new ColonyForResponse
                   {
                       ColonyId = Guid.NewGuid()
                   };
        }

        protected override IColony CreateEntity()
        {
            return new MicroServices.Colony.DataAccess.Colony
                   {
                       ColonyId = Guid.NewGuid()
                   };
        }

        private void AssertResponseAgainstEntity(
            [NotNull] IColonyForResponse response,
            [NotNull] IColony entity)
        {
            Assert.AreEqual(response.ColonyId,
                            entity.ColonyId,
                            "ColonyId");
            Assert.AreEqual(response.Description,
                            entity.Description,
                            "Description");
            Assert.AreEqual(response.Status,
                            entity.Status.ToString(),
                            "Status");
        }

        private void AssertEntityAgainstResponse(
            [NotNull] IColony entity,
            [NotNull] IColonyForResponse response)
        {
            AssertResponseAgainstEntity(response,
                                        entity);
        }

        [Test]
        public void ConvertStringToStatus_CallsLogger_ForunknownString()
        {
            // Arrange
            var logger = Substitute.For <ISelkieLogger>();
            var sut = new ColonyInformationFinder(logger,
                                                  Substitute.For <IColonyRepository>());

            // Act
            ColonyProgress.Status actual = sut.ConvertStringToStatus("ABCD");

            // Assert
            logger.Received().Warn(Arg.Any <string>());
        }

        [Test]
        [TestCase("Unknown", ColonyProgress.Status.Unknown)]
        [TestCase("Creating", ColonyProgress.Status.Creating)]
        [TestCase("Created", ColonyProgress.Status.Created)]
        [TestCase("Starting", ColonyProgress.Status.Starting)]
        [TestCase("Started", ColonyProgress.Status.Started)]
        [TestCase("Running", ColonyProgress.Status.Running)]
        [TestCase("Stopping", ColonyProgress.Status.Stopping)]
        [TestCase("Stopped", ColonyProgress.Status.Stopped)]
        [TestCase("Finished", ColonyProgress.Status.Finished)]
        [TestCase("ABCD", ColonyProgress.Status.Unknown)]
        public void ConvertStringToStatus_ReturnsStatus_ForKnownString(
            [NotNull] string statusAsText,
            [NotNull] ColonyProgress.Status expected)
        {
            // Arrange
            var sut = new ColonyInformationFinder(Substitute.For <ISelkieLogger>(),
                                                  Substitute.For <IColonyRepository>());

            // Act
            ColonyProgress.Status actual = sut.ConvertStringToStatus(statusAsText);

            // Assert
            Assert.AreEqual(expected,
                            actual);
        }

        [Test]
        [AutoNSubstituteData]
        public void ToEntity_ReturnsEntity_WhenCalled([NotNull] ColonyInformationFinder sut)
        {
            // Arrange
            IColonyForResponse response = CreateResponse();

            // Act
            IColony actual = sut.ToEntity(response);

            // Assert
            AssertResponseAgainstEntity(response,
                                        actual);
        }

        [Test]
        public void ToEntityForResponse_ReturnsEntityForResponse_WhenCalled()
        {
            // Arrange
            IColony entity = CreateEntity();

            // Act
            ColonyForResponse actual = ColonyInformationFinder.ToEntityForResponse(entity);

            // Assert
            AssertEntityAgainstResponse(entity,
                                        actual);
        }

        [Test]
        public void ToResponses_ReturnsResponses_ForEntities()
        {
            // Arrange
            IColony one = CreateEntity();
            IColony two = CreateEntity();
            var entities = new[]
                           {
                               one,
                               two
                           };

            // Act
            IEnumerable <ColonyForResponse> actual = ColonyInformationFinder.ToResponses(entities);

            // Assert
            Assert.AreEqual(2,
                            actual.Count());
        }
    }
}