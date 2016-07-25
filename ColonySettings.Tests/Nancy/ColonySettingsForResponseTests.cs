using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Selkie.Web.MicroServices.ColonySettings.Nancy;

namespace Selkie.Web.MicroServices.ColonySettings.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ColonySettingsForResponseTests
    {
        [Test]
        public void DefaultConstructor_SetsColonyId_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new ColonySettingsForResponse();

            // Assert
            Assert.AreEqual(0,
                            sut.ColonyId);
        }

        [Test]
        public void DefaultConstructor_SetsColonySettingsId_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new ColonySettingsForResponse();

            // Assert
            Assert.AreEqual(0,
                            sut.ColonySettingsId);
        }

        [Test]
        public void DefaultConstructor_SetsCostMatrix_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new ColonySettingsForResponse();

            // Assert
            Assert.NotNull(sut.CostMatrix);
        }

        [Test]
        public void DefaultConstructor_SetsCostPerFeature_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new ColonySettingsForResponse();

            // Assert
            Assert.NotNull(sut.CostPerFeature);
        }

        [Test]
        public void DefaultConstructor_SetsFixedStartNode_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new ColonySettingsForResponse();

            // Assert
            Assert.AreEqual(0,
                            sut.FixedStartNode);
        }

        [Test]
        public void DefaultConstructor_SetsIsFixedStartNode_WhenCalled()
        {
            // Arrange
            // Act
            var sut = new ColonySettingsForResponse();

            // Assert
            Assert.False(sut.IsFixedStartNode);
        }

        [Test]
        public void Id_ReturnsColonySettingsId_WhenCalled()
        {
            // Arrange
            var sut = new ColonySettingsForResponse();

            // Act
            sut.ColonySettingsId = 123;

            // Assert
            Assert.AreEqual(123,
                            sut.Id);
        }

        [Test]
        public void Id_SetsColonySettingsId_WhenCalled()
        {
            // Arrange
            var sut = new ColonySettingsForResponse();

            // Act
            sut.Id = 123;

            // Assert
            Assert.AreEqual(123,
                            sut.ColonySettingsId);
        }
    }
}