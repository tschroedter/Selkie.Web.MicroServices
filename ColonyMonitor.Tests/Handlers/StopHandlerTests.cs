using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Handlers;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces.Handlers;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Handlers
{
    [TestFixture]
    internal sealed class StopHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsUpdateStatus_WhenCalled(
            [NotNull, Frozen] IColonyManager manager,
            [NotNull] StopRequestMessage message,
            [NotNull] StopHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            manager.Received().UpdateStatus(message.ColonyId,
                                            ColonyProgress.Status.Stopping);
        }

        [Test]
        public void Constructor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            var manager = Substitute.For <IColonyManager>();

            // Act
            var sut = new StopHandler(manager);

            // Assert
            Assert.NotNull(sut);
        }
    }
}