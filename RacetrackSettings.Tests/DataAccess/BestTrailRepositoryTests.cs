using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.RacetrackSettings.DataAccess;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.Colony.Tests.DataAccess
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class RacetrackSettingsRepositoryTests
    {
        [Test]
        public void Constructor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            var context = Substitute.For <IRacetrackSettingsContext>();

            // Act
            var sut = new RacetrackSettingsRepository(context);

            // Assert
            Assert.NotNull(sut);
        }
    }
}