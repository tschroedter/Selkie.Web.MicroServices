using System;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Services.Common.Dto;
using Selkie.Services.Racetracks.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Entities.Manager
{
    [TestFixture]
    internal sealed class SurveyFeaturesManagerTests
    {
        private static void AssertDto(
            Guid expectedColonyId,
            SurveyFeatureDto expected,
            Web.MicroServices.ColonyMonitor.Dtos.SurveyFeatureDto actual)
        {
            Assert.AreEqual(expectedColonyId,
                            actual.ColonyId,
                            "ColonyId");
            Assert.AreEqual(expected.EndPoint.Y,
                            actual.EndPointY,
                            "EndPointY");
            Assert.AreEqual(expected.EndPoint.X,
                            actual.EndPointX,
                            "EndPointX");
            Assert.AreEqual(expected.RunDirection,
                            actual.RunDirection,
                            "RunDirection");
            Assert.AreEqual(expected.StartPoint.Y,
                            actual.StartPointY,
                            "StartPointY");
            Assert.AreEqual(expected.StartPoint.X,
                            actual.StartPointX,
                            "StartPointX");
            Assert.AreEqual(expected.AngleToXAxisAtEndPoint,
                            actual.AngleToXAxisAtEndPoint,
                            "AngleToXAxisAtEndPoint");
            Assert.AreEqual(expected.AngleToXAxisAtStartPoint,
                            actual.AngleToXAxisAtStartPoint,
                            "AngleToXAxisAtStartPoint");
            Assert.AreEqual(expected.IsUnknown,
                            actual.IsUnknown,
                            "IsUnknown");
            Assert.AreEqual(expected.Length,
                            actual.Length,
                            "Length");
        }

        [Test]
        [AutoNSubstituteData]
        public void Constructor_ReturnsINstance_WhenCalled(
            [NotNull, Frozen] ICrudSurveyFeatureDto crud)
        {
            // Arrange
            // Act
            var sut = new SurveyFeaturesManager(crud);

            // Assert
            Assert.NotNull(sut);
        }

        [Test]
        [AutoNSubstituteData]
        public void Create_CallsCreateOrUpdateForEachDto_ForMessage(
            [NotNull] CostMatrixCalculateMessage message,
            [NotNull] SurveyFeatureDto dtoOne,
            [NotNull] SurveyFeatureDto dtoTwo,
            [NotNull, Frozen] ICrudSurveyFeatureDto crud,
            [NotNull] SurveyFeaturesManager sut)
        {
            // Arrange
            message.SurveyFeatureDtos = new[]
                                        {
                                            dtoOne,
                                            dtoTwo
                                        };

            // Act
            sut.Create(message);

            // Assert
            crud.Received(2).CreateOrUpdate(Arg.Any <Web.MicroServices.ColonyMonitor.Dtos.SurveyFeatureDto>());
        }

        [Test]
        [AutoNSubstituteData]
        public void CreateSurveyFeatureDto_ReturnsDto_WhenCalled(
            Guid colonyId,
            [NotNull] SurveyFeatureDto dto)
        {
            // Arrange
            // Act
            Web.MicroServices.ColonyMonitor.Dtos.SurveyFeatureDto actual =
                SurveyFeaturesManager.CreateSurveyFeatureDto(colonyId,
                                                             dto);

            // Assert
            AssertDto(colonyId,
                      dto,
                      actual);
        }
    }
}