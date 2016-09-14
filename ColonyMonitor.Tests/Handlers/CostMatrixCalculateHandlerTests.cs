using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Racetracks.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager;
using Selkie.Web.MicroServices.ColonyMonitor.Handlers;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Handlers
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class CostMatrixCalculateHandlerTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsCreateOnRacetrackSettingsManager_WhenCalled(
            [NotNull, Frozen] IRacetrackSettingsManager manager,
            [NotNull] CostMatrixCalculateMessage message,
            [NotNull] CostMatrixCalculateHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            manager.Received().Create(message);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Handle_CallsCreateOnSurveyFeaturesManager_WhenCalled(
            [NotNull, Frozen] ISurveyFeaturesManager manager,
            [NotNull] CostMatrixCalculateMessage message,
            [NotNull] CostMatrixCalculateHandler sut)
        {
            // Arrange
            // Act
            sut.Handle(message);

            // Assert
            manager.Received().Create(message);
        }

        [Test]
        [AutoNSubstituteData]
        public void Constructor_ReturnsInstance_WhenCalled(
            [NotNull] IRacetrackSettingsManager racetrackSettingsManager,
            [NotNull] ISurveyFeaturesManager surveyFeaturesManager)
        {
            // Arrange
            // Act
            var sut = new CostMatrixCalculateHandler(racetrackSettingsManager,
                                                     surveyFeaturesManager);

            // Assert
            Assert.NotNull(sut);
        }
    }
}