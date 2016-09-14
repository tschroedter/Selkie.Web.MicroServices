using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Selkie.Web.MicroServices.SurveyFeature.Tests.DataAccess
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SurveyFeatureTests
    {
        [Test]
        public void Constructor_SetsEndPointX_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new SurveyFeature.DataAccess.SurveyFeature();

            // Assert
            Assert.AreEqual(SurveyFeature.DataAccess.SurveyFeature.DefaultCoordinate,
                            sut.EndPointX);
        }

        [Test]
        public void Constructor_SetsEndPointY_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new SurveyFeature.DataAccess.SurveyFeature();

            // Assert
            Assert.AreEqual(SurveyFeature.DataAccess.SurveyFeature.DefaultCoordinate,
                            sut.EndPointY);
        }

        [Test]
        public void Constructor_SetsRunDirection_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new SurveyFeature.DataAccess.SurveyFeature();

            // Assert
            Assert.AreEqual(string.Empty,
                            sut.RunDirection);
        }

        [Test]
        public void Constructor_SetsStartPointX_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new SurveyFeature.DataAccess.SurveyFeature();

            // Assert
            Assert.AreEqual(SurveyFeature.DataAccess.SurveyFeature.DefaultCoordinate,
                            sut.StartPointX);
        }

        [Test]
        public void Constructor_SetsStartPointY_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new SurveyFeature.DataAccess.SurveyFeature();

            // Assert
            Assert.AreEqual(SurveyFeature.DataAccess.SurveyFeature.DefaultCoordinate,
                            sut.StartPointY);
        }

        [Test]
        public void Id_ReturnsBestTrailId_WhenCalled()
        {
            // Arrange
            Guid expected = Guid.Parse("00000000-0000-0000-0000-000000000123");
            var sut = new SurveyFeature.DataAccess.SurveyFeature();

            // Act
            sut.SurveyFeatureId = expected;

            // Assert
            Assert.AreEqual(expected,
                            sut.Id);
        }

        [Test]
        public void Id_SetsBestTrailId_WhenCalled()
        {
            // Arrange
            Guid expected = Guid.Parse("00000000-0000-0000-0000-000000000123");
            var sut = new SurveyFeature.DataAccess.SurveyFeature();

            // Act
            sut.Id = expected;

            // Assert
            Assert.AreEqual(expected,
                            sut.SurveyFeatureId);
        }
    }
}