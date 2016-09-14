using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Entities.Manager
{
    [TestFixture]
    internal sealed class ColonySettingsManagerTests
    {
        [Test]
        [AutoNSubstituteData]
        public void Constructor_ReturnsINstance_WhenCalled(
            [NotNull, Frozen] ICrudColonySettingsDto crud)
        {
            // Arrange
            // Act
            var sut = new ColonySettingsManager(crud);

            // Assert
            Assert.NotNull(sut);
        }

        [Test]
        [AutoNSubstituteData]
        public void Create_CallsCreateOrUpdate_ForMessages(
            [NotNull] CreateColonyMessage message,
            [NotNull, Frozen] ICrudColonySettingsDto crud,
            [NotNull] ColonySettingsManager sut)
        {
            // Arrange
            // Act
            sut.Create(message);

            // Assert
            crud.Received().CreateOrUpdate(Arg.Is <ColonySettingsDto>(x =>
                                                                      x.ColonyId == message.ColonyId &&
                                                                      x.CostMatrix.Length == message.CostMatrix.Length &&
                                                                      x.CostPerFeature.SequenceEqual(
                                                                                                     message
                                                                                                         .CostPerFeature) &&
                                                                      x.FixedStartNode == message.FixedStartNode &&
                                                                      x.IsFixedStartNode == message.IsFixedStartNode));
        }

        [Test]
        [AutoNSubstituteData]
        public void Create_ReturnsDto_ForMessages(
            [NotNull] CreateColonyMessage message,
            [NotNull] ColonySettingsDto dto,
            [NotNull, Frozen] ICrudColonySettingsDto crud,
            [NotNull] ColonySettingsManager sut)
        {
            // Arrange
            crud.CreateOrUpdate(Arg.Any <ColonySettingsDto>()).Returns(dto);

            // Act
            ColonySettingsDto actual = sut.Create(message);

            // Assert
            Assert.AreEqual(dto,
                            actual);
        }
    }
}