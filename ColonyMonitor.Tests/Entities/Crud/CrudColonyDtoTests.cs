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
    internal sealed class CrudColonyDtoTests
    {
        [Test]
        [AutoNSubstituteData]
        public void MicroServiceName_ReturnsName_UnderCondition(
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory)
        {
            // Arrange
            // Act
            var sut = new CrudColonyDto(reader,
                                        factory);

            // Assert
            Assert.AreEqual("/colonies/",
                            sut.MicroServiceName);
        }
    }
}