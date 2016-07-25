using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.ColonySettings.DataAccess;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.ColonySettings.Tests.DataAccess
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ColonySettingsRepositoryTests
    {
        [Test]
        public void Constructor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            var context = Substitute.For <IColonySettingsContext>();

            // Act
            var sut = new ColonySettingsRepository(context);

            // Assert
            Assert.NotNull(sut);
        }
    }
}