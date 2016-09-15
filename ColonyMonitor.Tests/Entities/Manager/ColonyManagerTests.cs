using System;
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
    internal sealed class ColonyManagerTests
    {
        [Test]
        [AutoNSubstituteData]
        public void Constructor_ReturnsINstance_WhenCalled(
            [NotNull, Frozen] ICrudColonyDto crud)
        {
            // Arrange
            // Act
            var sut = new ColonyManager(crud);

            // Assert
            Assert.NotNull(sut);
        }

        [Test]
        [AutoNSubstituteData]
        public void Create_CallsCreateOrUpdate_ForMessages(
            [NotNull] CreateColonyMessage message,
            [NotNull, Frozen] ICrudColonyDto crud,
            [NotNull] ColonyManager sut)
        {
            // Arrange
            // Act
            sut.Create(message);

            // Assert
            crud.Received().CreateOrUpdate(Arg.Is <ColonyDto>(x =>
                                                              x.ColonyId == message.ColonyId &&
                                                              x.Description == "CreateColonyMessage" &&
                                                              x.Status == ColonyProgress.Status.Creating));
        }

        [Test]
        [AutoNSubstituteData]
        public void Create_CallsCreateOrUpdate_ForMessages(
            [NotNull] CreateColonyMessage message,
            [NotNull] ColonyDto dto,
            [NotNull, Frozen] ICrudColonyDto crud,
            [NotNull] ColonyManager sut)
        {
            // Arrange
            crud.CreateOrUpdate(Arg.Any <ColonyDto>()).Returns(dto);

            // Act
            ColonyDto actual = sut.Create(message);

            // Assert
            Assert.AreEqual(dto,
                            actual);
        }

        [Test]
        [AutoNSubstituteData]
        public void Created_CallsCreateOrUpdate_ForMessages(
            [NotNull] CreatedColonyMessage message,
            [NotNull] ColonyDto dto,
            [NotNull, Frozen] ICrudColonyDto crud,
            [NotNull] ColonyManager sut)
        {
            // Arrange
            crud.Read(Arg.Any <Guid>()).Returns(dto);

            // Act
            sut.Created(message.ColonyId);

            // Assert
            crud.Received().CreateOrUpdate(Arg.Is <ColonyDto>(x =>
                                                              x.ColonyId == dto.ColonyId &&
                                                              x.Description == dto.Description &&
                                                              x.Status == ColonyProgress.Status.Created));
        }

        [Test]
        [AutoNSubstituteData]
        public void UpdateStatus_CallsCreateOrUpdate_ForMessages(
            [NotNull] ColonyDto dto,
            [NotNull, Frozen] ICrudColonyDto crud,
            [NotNull] ColonyManager sut)
        {
            // Arrange
            crud.Read(Arg.Any <Guid>()).Returns(dto);

            // Act
            sut.UpdateStatus(dto.ColonyId,
                             ColonyProgress.Status.Created);

            // Assert
            crud.Received().CreateOrUpdate(Arg.Is <ColonyDto>(x =>
                                                              x.ColonyId == dto.ColonyId &&
                                                              x.Description == dto.Description &&
                                                              x.Status == ColonyProgress.Status.Created));
        }

        [Test]
        [AutoNSubstituteData]
        public void UpdateStatus_CallsRead_ForMessages(
            Guid colonyId,
            [NotNull, Frozen] ICrudColonyDto crud,
            [NotNull] ColonyManager sut)
        {
            // Arrange
            // Act
            sut.UpdateStatus(colonyId,
                             ColonyProgress.Status.Created);


            // Assert
            crud.Received().Read(colonyId);
        }
    }
}