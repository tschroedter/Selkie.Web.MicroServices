using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Racetracks.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Entities.Manager
{
    [TestFixture]
    internal sealed class RacetrackSettingsManagerTests
    {
        [Test]
        [AutoNSubstituteData]
        public void Constructor_ReturnsINstance_WhenCalled(
            [NotNull, Frozen] ICrudRacetrackSettingsDto crud)
        {
            // Arrange
            // Act
            var sut = new RacetrackSettingsManager(crud);

            // Assert
            Assert.NotNull(sut);
        }

        [Test]
        [AutoNSubstituteData]
        public void Create_CallsCreateOrUpdate_ForMessages(
            [NotNull] CostMatrixCalculateMessage message,
            [NotNull, Frozen] ICrudRacetrackSettingsDto crud,
            [NotNull] RacetrackSettingsManager sut)
        {
            // Arrange
            // Act
            sut.Create(message);

            // Assert
            crud.Received().CreateOrUpdate(Arg.Is <RacetrackSettingsDto>(x =>
                                                                         x.ColonyId == message.ColonyId &&
                                                                         x.IsPortTurnAllowed ==
                                                                         message.IsPortTurnAllowed &&
                                                                         x.IsStarboardTurnAllowed ==
                                                                         message.IsStarboardTurnAllowed &&
                                                                         x.TurnRadiusForPort ==
                                                                         message.TurnRadiusForPort &&
                                                                         x.TurnRadiusForStarboard ==
                                                                         message.TurnRadiusForStarboard));
        }

        [Test]
        [AutoNSubstituteData]
        public void Create_ReturnsDto_ForMessages(
            [NotNull] CostMatrixCalculateMessage message,
            [NotNull] RacetrackSettingsDto dto,
            [NotNull, Frozen] ICrudRacetrackSettingsDto crud,
            [NotNull] RacetrackSettingsManager sut)
        {
            // Arrange
            crud.CreateOrUpdate(Arg.Any <RacetrackSettingsDto>()).Returns(dto);

            // Act
            RacetrackSettingsDto actual = sut.Create(message);

            // Assert
            Assert.AreEqual(dto,
                            actual);
        }
    }
}