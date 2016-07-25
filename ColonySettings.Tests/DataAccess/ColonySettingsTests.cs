using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Selkie.Web.MicroServices.ColonySettings.Tests.DataAccess
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ColonySettingsTests
    {
        [Test]
        public void CostMatrix_ReturnsCostMatrix_ForCostMatrixInternalData()
        {
            // Arrange
            var expected = new[]
                           {
                               new[]
                               {
                                   1,
                                   2
                               },
                               new[]
                               {
                                   1,
                                   2
                               }
                           };

            var sut = new ColonySettings.DataAccess.ColonySettings();
            sut.CostMatrix = expected;

            // Act

            // Assert
            Assert.AreEqual(expected,
                            sut.CostMatrix);
        }

        [Test]
        public void CostMatrix_SetsCostMatrixInternalData_WhenCalled()
        {
            // Arrange
            var costMatrix = new[]
                             {
                                 new[]
                                 {
                                     1,
                                     2
                                 },
                                 new[]
                                 {
                                     1,
                                     2
                                 }
                             };

            var sut = new ColonySettings.DataAccess.ColonySettings();

            // Act
            sut.CostMatrix = costMatrix;

            // Assert
            Assert.AreEqual("1,2:1,2",
                            sut.CostMatrixInternalData);
        }

        [Test]
        public void CostPerFeature_ReturnsCostPerFeature_ForCostPerFeatureInternalData()
        {
            // Arrange
            var expected = new[]
                           {
                               1,
                               2
                           };

            var sut = new ColonySettings.DataAccess.ColonySettings();

            // Act
            sut.CostPerFeature = expected;

            // Assert
            Assert.AreEqual(expected,
                            sut.CostPerFeature);
        }

        [Test]
        public void CostPerFeature_SetsCostPerFeatureInternalData_WhenCalled()
        {
            // Arrange
            var costPerFeature = new[]
                                 {
                                     1,
                                     2
                                 };

            var sut = new ColonySettings.DataAccess.ColonySettings();

            // Act
            sut.CostPerFeature = costPerFeature;

            // Assert
            Assert.AreEqual("1,2",
                            sut.CostPerFeatureInternalData);
        }

        [Test]
        public void Id_ReturnsColonySettingsId_WhenCalled()
        {
            // Arrange
            var sut = new ColonySettings.DataAccess.ColonySettings();

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
            var sut = new ColonySettings.DataAccess.ColonySettings();

            // Act
            sut.Id = 123;

            // Assert
            Assert.AreEqual(123,
                            sut.ColonySettingsId);
        }
    }
}