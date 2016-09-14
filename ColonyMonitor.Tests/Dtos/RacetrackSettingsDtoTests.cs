using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Dtos
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class RacetrackSettingsDtoTests
    {
        [Test]
        public void Constructor_SetsIsPortTurnAllowed_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new RacetrackSettingsDto();

            // Assert
            Assert.True(sut.IsPortTurnAllowed);
        }

        [Test]
        public void Constructor_SetsIsStarboardTurnAllowed_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new RacetrackSettingsDto();

            // Assert
            Assert.True(sut.IsStarboardTurnAllowed);
        }

        [Test]
        public void Constructor_SetsTurnRadiusForPort_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new RacetrackSettingsDto();

            // Assert
            NUnitHelper.AssertIsEquivalent(30.0,
                                           sut.TurnRadiusForPort);
        }

        [Test]
        public void Constructor_SetsTurnRadiusForStarboard_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new RacetrackSettingsDto();

            // Assert
            NUnitHelper.AssertIsEquivalent(30.0,
                                           sut.TurnRadiusForStarboard);
        }
    }
}