using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Web.MicroServices.Colony.DataAccess;

namespace Selkie.Web.MicroServices.Colony.Tests.DataAccess
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ColonyTests
    {
        [Test]
        public void Constructor_SetsStatus_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new Colony.DataAccess.Colony();

            // Assert
            Assert.AreEqual(ColonyProgress.Status.Unknown,
                            sut.Status);
        }

        [Test]
        public void Constructor_SetsTrail_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new Colony.DataAccess.Colony();

            // Assert
            Assert.AreEqual(string.Empty,
                            sut.Description);
        }

        [Test]
        public void Id_ReturnsBestTrailId_WhenCalled()
        {
            // Arrange
            var sut = new Colony.DataAccess.Colony();

            // Act
            sut.ColonyId = 123;

            // Assert
            Assert.AreEqual(123,
                            sut.Id);
        }

        [Test]
        public void Id_SetsBestTrailId_WhenCalled()
        {
            // Arrange
            var sut = new Colony.DataAccess.Colony();

            // Act
            sut.Id = 123;

            // Assert
            Assert.AreEqual(123,
                            sut.ColonyId);
        }
    }
}