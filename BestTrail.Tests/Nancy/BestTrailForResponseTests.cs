using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NUnit.Framework;
using Selkie.Web.MicroServices.BestTrail.Nancy;

namespace Selkie.Web.MicroServices.BestTrail.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class BestTrailForResponseTests
    {
        [Test]
        public void Constructor_SetsTrail_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new BestTrailForResponse();

            // Assert
            Assert.AreEqual(0,
                            sut.Trail.Count());
        }

        [Test]
        public void Id_ReturnsBestTrailId_WhenCalled()
        {
            // Arrange
            Guid expected = Guid.Parse("00000000-0000-0000-0000-000000000123");
            var sut = new BestTrailForResponse();

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
            var sut = new BestTrailForResponse();

            // Act
            sut.Id = expected;

            // Assert
            Assert.AreEqual(expected,
                            sut.BestTrailId);
        }
    }
}