using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Handlers;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces.Handlers;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Handlers
{
    [TestFixture]
    internal sealed class FinishedHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsFinished_WhenCalled(
            [NotNull, Frozen] IColonyManager manager,
            [NotNull] FinishedMessage message,
            [NotNull] FinishedHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            manager.Received().Finished(message);
        }

        [Test]
        public void Constructor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            var manager = Substitute.For <IColonyManager>();

            // Act
            var sut = new FinishedHandler(manager);

            // Assert
            Assert.NotNull(sut);
        }
    }
}