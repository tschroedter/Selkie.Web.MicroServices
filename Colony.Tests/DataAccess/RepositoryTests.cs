using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.Colony.DataAccess;
using Selkie.Web.MicroServices.Colony.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.Colony.Tests.DataAccess
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class RepositoryTests
    {
        [Test]
        public void Constructor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            var context = Substitute.For <IColonyContext>();

            // Act
            var sut = new ColonyRepository(context);

            // Assert
            Assert.NotNull(sut);
        }
    }
}