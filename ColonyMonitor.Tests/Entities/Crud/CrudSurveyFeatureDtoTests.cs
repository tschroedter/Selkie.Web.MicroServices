using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud.Rest;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Entities.Crud
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class CrudSurveyFeatureDtoTests
    {
        [Test]
        [AutoNSubstituteData]
        public void MicroServiceName_ReturnsName_UnderCondition(
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory)
        {
            // Arrange
            // Act
            var sut = new CrudSurveyFeatureDto(reader,
                                               factory);

            // Assert
            Assert.AreEqual("/surveyfeatures/",
                            sut.MicroServiceName);
        }
    }
}