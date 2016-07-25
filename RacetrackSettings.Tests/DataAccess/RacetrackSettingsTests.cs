using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Web.MicroServices.RacetrackSettings.Nancy;

namespace Selkie.Web.MicroServices.RacetrackSettings.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class RacetrackSettingsTests
    {
        [Test]
        public void Constructor_SetsIsPortTurnAllowed_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new DataAccess.RacetrackSettings();

            // Assert
            Assert.True(sut.IsPortTurnAllowed);
        }

        [Test]
        public void Constructor_SetsIsStarboardTurnAllowed_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new DataAccess.RacetrackSettings();

            // Assert
            Assert.True(sut.IsStarboardTurnAllowed);
        }

        [Test]
        public void Constructor_SetsTurnRadiusForPort_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new DataAccess.RacetrackSettings();

            // Assert
            Assert.AreEqual(RacetrackSettingsForResponse.DefaultTurnRadius,
                            sut.TurnRadiusForPort);
        }

        [Test]
        public void Constructor_SetsTurnRadiusForStarboard_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new DataAccess.RacetrackSettings();

            // Assert
            Assert.AreEqual(RacetrackSettingsForResponse.DefaultTurnRadius,
                            sut.TurnRadiusForStarboard);
        }
    }
}