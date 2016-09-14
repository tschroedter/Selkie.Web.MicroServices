using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Dtos
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SurveyFeatureDtoTests
    {
        [Test]
        public void Constructor_SetsRunDirection_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new SurveyFeatureDto();

            // Assert
            Assert.AreEqual(string.Empty,
                            sut.RunDirection);
        }
    }
}