using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.SurveyFeature.DataAccess;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.BestTrail.Tests.DataAccess
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SurveyFeatureRepositoryTests
    {
        [Test]
        public void Constructor_ReturnsInstance_WhenCalled()
        {
            // Arrange
            var context = Substitute.For <ISurveyFeatureContext>();

            // Act
            var sut = new SurveyFeatureRepository(context);

            // Assert
            Assert.NotNull(sut);
        }
    }
}