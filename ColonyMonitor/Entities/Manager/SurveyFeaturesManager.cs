using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Services.Racetracks.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager
{
    [ProjectComponent(Lifestyle.Transient)] // todo logger aspect
    public class SurveyFeaturesManager : ISurveyFeaturesManager
    {
        public SurveyFeaturesManager([NotNull] ICrudSurveyFeatureDto crud)
        {
            m_Crud = crud;
        }

        private readonly ICrudSurveyFeatureDto m_Crud;

        public IEnumerable <SurveyFeatureDto> Create(CostMatrixCalculateMessage message)
        {
            IEnumerable <SurveyFeatureDto> dtos = CreateDtos(message);
            IEnumerable <SurveyFeatureDto> updatedDtos = Save(dtos);

            return updatedDtos;
        }

        internal static SurveyFeatureDto CreateSurveyFeatureDto(Guid colonyId,
                                                                Services.Common.Dto.SurveyFeatureDto dto)
        {
            var localDto = new SurveyFeatureDto
                           {
                               ColonyId = colonyId,
                               EndPointY = dto.EndPoint.Y,
                               EndPointX = dto.EndPoint.X,
                               RunDirection = dto.RunDirection,
                               StartPointY = dto.StartPoint.Y,
                               StartPointX = dto.StartPoint.X,
                               AngleToXAxisAtEndPoint = dto.AngleToXAxisAtEndPoint,
                               AngleToXAxisAtStartPoint = dto.AngleToXAxisAtStartPoint,
                               IsUnknown = dto.IsUnknown,
                               Length = dto.Length
                           };

            return localDto;
        }

        private static IEnumerable <SurveyFeatureDto> CreateDtos(
            [NotNull] CostMatrixCalculateMessage message)
        {
            return message.SurveyFeatureDtos
                          .Select(surveyFeatureDto => CreateSurveyFeatureDto(message.ColonyId,
                                                                             surveyFeatureDto))
                          .ToList();
        }

        private IEnumerable <SurveyFeatureDto> Save(IEnumerable <SurveyFeatureDto> dtos)
        {
            return dtos.Select(dto => m_Crud.CreateOrUpdate(dto))
                       .ToList();
        }
    }
}