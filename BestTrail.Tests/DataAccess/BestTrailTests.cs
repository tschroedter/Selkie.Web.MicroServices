using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;

namespace Selkie.Web.MicroServices.BestTrail.Tests.DataAccess
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class BestTrailTests
    {
        [Test]
        public void Constructor_SetsTrail_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new BestTrail.DataAccess.BestTrail();

            // Assert
            Assert.AreEqual(0,
                            sut.Trail.Count());
        }

        [Test]
        public void Constructor_SetsTrailInternalData_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new BestTrail.DataAccess.BestTrail();

            // Assert
            Assert.AreEqual(string.Empty,
                            sut.TrailInternalData);
        }

        [Test]
        public void Constructor_SetsType_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new BestTrail.DataAccess.BestTrail();

            // Assert
            Assert.AreEqual("Unknown",
                            sut.Type);
        }

        [Test]
        public void Id_ReturnsBestTrailId_WhenCalled()
        {
            // Arrange
            Guid expected = Guid.Parse("00000000-0000-0000-0000-000000000123");
            var sut = new BestTrail.DataAccess.BestTrail();

            // Act
            sut.BestTrailId = expected;

            // Assert
            Assert.AreEqual(expected,
                            sut.Id);
        }

        [Test]
        public void Id_SetsBestTrailId_WhenCalled()
        {
            // Arrange
            Guid expected = Guid.Parse("00000000-0000-0000-0000-000000000123");
            var sut = new BestTrail.DataAccess.BestTrail();

            // Act
            sut.Id = expected;

            // Assert
            Assert.AreEqual(expected,
                            sut.BestTrailId);
        }

        [Test]
        public void Trail_SetsTrailInternalData_ForGivenArray()
        {
            // Arrange
            var sut = new BestTrail.DataAccess.BestTrail();

            // Act
            sut.Trail = new[]
                        {
                            1,
                            2,
                            3
                        };

            // Assert
            Assert.AreEqual("1,2,3",
                            sut.TrailInternalData);
        }

        [Test]
        public void Trail_SetsTrailInternalData_ForGivenArrayWithOneElement()
        {
            // Arrange
            var sut = new BestTrail.DataAccess.BestTrail();

            // Act
            sut.Trail = new[]
                        {
                            1
                        };

            // Assert
            Assert.AreEqual("1",
                            sut.TrailInternalData);
        }

        [Test]
        public void Trail_SetsTrailInternalData_ForGivenEmptyArray()
        {
            // Arrange
            var sut = new BestTrail.DataAccess.BestTrail();

            // Act
            sut.Trail = new int[0];

            // Assert
            Assert.AreEqual(string.Empty,
                            sut.TrailInternalData);
        }
    }
}