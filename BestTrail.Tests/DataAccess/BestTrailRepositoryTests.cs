using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.BestTrail.DataAccess;
using Selkie.Web.MicroServices.BestTrail.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.BestTrail.Tests.DataAccess
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class BestTrailRepositoryTests
    {
        [Test]
        public void Constructor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            var context = Substitute.For <IBestTrailContext>();

            // Act
            var sut = new BestTrailRepository(context);

            // Assert
            Assert.NotNull(sut);
        }
    }
}