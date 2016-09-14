using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager;
using Selkie.Web.MicroServices.ColonyMonitor.Handlers;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces.Handlers;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Handlers
{
    [TestFixture]
    internal sealed class CreateColonyHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsColonyManagersCreate_WhenCalled(
            [NotNull, Frozen] IColonyManager manager,
            [NotNull] CreateColonyMessage message,
            [NotNull] CreateColonyHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            manager.Received().Create(message);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsColonySettingsManagersCreate_WhenCalled(
            [NotNull, Frozen] IColonySettingsManager manager,
            [NotNull] CreateColonyMessage message,
            [NotNull] CreateColonyHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            manager.Received().Create(message);
        }

        [Test]
        public void Constructor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            var colonyManager = Substitute.For <IColonyManager>();
            var colonySettingsManager = Substitute.For <IColonySettingsManager>();

            // Act
            var sut = new CreateColonyHandler(colonyManager,
                                              colonySettingsManager);

            // Assert
            Assert.NotNull(sut);
        }
    }
}