using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Web.MicroServices.Colony.Nancy;

namespace Selkie.Web.MicroServices.Colony.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ColonyForResponseTests
    {
        [Test]
        public void DefaultConstructor_SetsDescription_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new ColonyForResponse();

            // Assert
            Assert.AreEqual(string.Empty,
                            sut.Description);
        }

        [Test]
        public void DefaultConstructor_SetsId_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new ColonyForResponse();

            // Assert
            Assert.AreEqual(Guid.Empty,
                            sut.ColonyId);
        }

        [Test]
        public void DefaultConstructor_SetsStatus_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new ColonyForResponse();

            // Assert
            Assert.AreEqual("Unknown",
                            sut.Status);
        }

        [Test]
        public void Id_ReturnsBestTrailId_WhenCalled()
        {
            // Arrange
            Guid expected = Guid.Parse("00000000-0000-0000-0000-000000000123");
            var sut = new ColonyForResponse();

            // Act
            sut.ColonyId = expected;

            // Assert
            Assert.AreEqual(expected,
                            sut.Id);
        }

        [Test]
        public void Id_SetsBestTrailId_WhenCalled()
        {
            // Arrange
            Guid expected = Guid.Parse("00000000-0000-0000-0000-000000000123");
            var sut = new ColonyForResponse();

            // Act
            sut.Id = expected;

            // Assert
            Assert.AreEqual(expected,
                            sut.ColonyId);
        }
    }
}