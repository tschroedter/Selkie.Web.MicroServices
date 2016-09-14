using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager;
using Selkie.Web.MicroServices.ColonyMonitor.Handlers;
using Selkie.Windsor;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Handlers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class BestTrailHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsCreate_ForValidColonyId(
            [NotNull, Frozen] IBestTrailManager manager,
            [NotNull] BestTrailMessage message,
            [NotNull] BestTrailHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            manager.Received().Create(message);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Handle_DoesNotCallsCreate_ForColonyIdIsEmpty(
            [NotNull, Frozen] IBestTrailManager manager,
            [NotNull] BestTrailMessage message,
            [NotNull] BestTrailHandler sut)
        {
            // Arrange
            message.ColonyId = Guid.Empty;

            // Act
            sut.Handle(message);

            // Assert
            manager.DidNotReceive().Create(message);
        }

        [Test]
        [AutoNSubstituteData]
        public void Constructor_ReturnsInstance_ForValidColonyId(
            [NotNull] ISelkieLogger logger,
            [NotNull] IBestTrailManager manager)
        {
            // Arrange
            // Act
            var sut = new BestTrailHandler(logger,
                                           manager);

            // Assert
            Assert.NotNull(sut);
        }
    }
}