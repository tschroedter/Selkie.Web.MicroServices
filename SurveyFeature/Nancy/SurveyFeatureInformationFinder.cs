using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.DataAccess;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.SurveyFeature.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SurveyFeatureInformationFinder : ISurveyFeatureInformationFinder
    {
        public SurveyFeatureInformationFinder([NotNull] ISurveyFeatureRepository repository)
        {
            m_Repository = repository;
        }

        private readonly ISurveyFeatureRepository m_Repository;

        public ISurveyFeatureForResponse FindById(int id)
        {
            ISurveyFeature entity = m_Repository.FindById(id);

            return entity != null
                       ? ToEntityForResponse(entity)
                       : null;
        }

        public IEnumerable <ISurveyFeatureForResponse> List()
        {
            IEnumerable <ISurveyFeature> all = m_Repository.All;

            IEnumerable <SurveyFeatureForResponse> items = ToResponses(all);

            return items;
        }

        public ISurveyFeatureForResponse Save(ISurveyFeatureForResponse response)
        {
            ISurveyFeature toBeUpdated = ToEntity(response);

            m_Repository.Save(toBeUpdated);

            return new SurveyFeatureForResponse
                   {
                       AngleToXAxisAtEndPoint = toBeUpdated.AngleToXAxisAtEndPoint,
                       AngleToXAxisAtStartPoint = toBeUpdated.AngleToXAxisAtStartPoint,
                       ColonyId = toBeUpdated.ColonyId,
                       EndPointX = toBeUpdated.EndPointX, // todo not nice here => return point???
                       EndPointY = toBeUpdated.EndPointY,
                       IsUnknown = toBeUpdated.IsUnknown,
                       Length = toBeUpdated.Length,
                       RunDirection = toBeUpdated.RunDirection,
                       StartPointX = toBeUpdated.StartPointX,
                       StartPointY = toBeUpdated.StartPointY,
                       SurveyFeatureId = toBeUpdated.SurveyFeatureId
                   };
        }

        public ISurveyFeatureForResponse Delete(int id)
        {
            ISurveyFeature entity = m_Repository.FindById(id);

            m_Repository.Remove(entity);

            return entity == null
                       ? null
                       : ToEntityForResponse(entity);
        }

        internal static ISurveyFeature ToEntity([NotNull] ISurveyFeatureForResponse response)
        {
            var entity = new DataAccess.SurveyFeature
                         {
                             AngleToXAxisAtEndPoint = response.AngleToXAxisAtEndPoint,
                             AngleToXAxisAtStartPoint = response.AngleToXAxisAtStartPoint,
                             ColonyId = response.ColonyId,
                             EndPointX = response.EndPointX,
                             EndPointY = response.EndPointY,
                             IsUnknown = response.IsUnknown,
                             Length = response.Length,
                             RunDirection = response.RunDirection,
                             StartPointX = response.StartPointX,
                             StartPointY = response.StartPointY,
                             SurveyFeatureId = response.SurveyFeatureId
                         };

            return entity;
        }

        internal static SurveyFeatureForResponse ToEntityForResponse(ISurveyFeature entity)
        {
            return new SurveyFeatureForResponse
                   {
                       AngleToXAxisAtEndPoint = entity.AngleToXAxisAtEndPoint,
                       AngleToXAxisAtStartPoint = entity.AngleToXAxisAtStartPoint,
                       ColonyId = entity.ColonyId,
                       EndPointX = entity.EndPointX,
                       EndPointY = entity.EndPointY,
                       IsUnknown = entity.IsUnknown,
                       Length = entity.Length,
                       RunDirection = entity.RunDirection,
                       StartPointX = entity.StartPointX,
                       StartPointY = entity.StartPointY,
                       SurveyFeatureId = entity.SurveyFeatureId
                   };
        }

        internal static IEnumerable <SurveyFeatureForResponse> ToResponses(IEnumerable <ISurveyFeature> all)
        {
            SurveyFeatureForResponse[] list = all.Select(ToEntityForResponse)
                                                 .ToArray();
            return list;
        }
    }
}