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
    internal sealed class BestTrailManagerTests
    {
        [Test]
        [AutoNSubstituteData]
        public void Constructor_ReturnsINstance_WhenCalled(
            [NotNull, Frozen] ICrudBestTrailDto crud)
        {
            // Arrange
            // Act
            var sut = new BestTrailManager(crud);

            // Assert
            Assert.NotNull(sut);
        }

        [Test]
        [AutoNSubstituteData]
        public void Create_CallsCreateOrUpdate_ForMessages(
            [NotNull] BestTrailMessage message,
            [NotNull, Frozen] ICrudBestTrailDto crud,
            [NotNull] BestTrailManager sut)
        {
            // Arrange
            // Act
            sut.Create(message);

            // Assert
            crud.Received().CreateOrUpdate(Arg.Is <BestTrailDto>(x =>
                                                                 x.Alpha == message.Alpha &&
                                                                 x.Beta == message.Beta &&
                                                                 x.ColonyId == message.ColonyId &&
                                                                 x.Gamma == message.Gamma &&
                                                                 x.Iteration == message.Iteration &&
                                                                 x.Length == message.Length &&
                                                                 x.Trail.SequenceEqual(message.Trail) &&
                                                                 x.Type == message.Type));
        }

        [Test]
        [AutoNSubstituteData]
        public void Create_ReturnsDto_ForMessages(
            [NotNull] BestTrailMessage message,
            [NotNull] BestTrailDto dto,
            [NotNull, Frozen] ICrudBestTrailDto crud,
            [NotNull] BestTrailManager sut)
        {
            // Arrange
            crud.CreateOrUpdate(Arg.Any <BestTrailDto>()).Returns(dto);

            // Act
            BestTrailDto actual = sut.Create(message);

            // Assert
            Assert.AreEqual(dto,
                            actual);
        }
    }
}