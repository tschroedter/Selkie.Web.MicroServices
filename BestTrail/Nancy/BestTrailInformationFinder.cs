using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.BestTrail.Interfaces.DataAccess;
using Selkie.Web.MicroServices.BestTrail.Interfaces.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.BestTrail.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class BestTrailInformationFinder : IBestTrailInformationFinder
    {
        public BestTrailInformationFinder([NotNull] IBestTrailRepository repository)
        {
            m_Repository = repository;
        }

        private readonly IBestTrailRepository m_Repository;

        public IBestTrailForResponse FindById(int id)
        {
            IBestTrail entity = m_Repository.FindById(id);

            return entity != null
                       ? ToEntityForResponse(entity)
                       : null;
        }

        public IEnumerable <IBestTrailForResponse> List()
        {
            IEnumerable <IBestTrail> all = m_Repository.All;

            IEnumerable <BestTrailForResponse> items = ToResponses(all);

            return items;
        }

        public IBestTrailForResponse Save(IBestTrailForResponse response)
        {
            IBestTrail toBeUpdated = ToEntity(response);

            m_Repository.Save(toBeUpdated);

            return new BestTrailForResponse
                   {
                       Alpha = toBeUpdated.Alpha,
                       BestTrailId = toBeUpdated.BestTrailId,
                       Beta = toBeUpdated.Beta,
                       ColonyId = toBeUpdated.ColonyId,
                       Gamma = toBeUpdated.Gamma,
                       Iteration = toBeUpdated.Iteration,
                       Length = toBeUpdated.Length,
                       Trail = toBeUpdated.Trail,
                       Type = toBeUpdated.Type
                   };
        }

        public IBestTrailForResponse Delete(int id)
        {
            IBestTrail entity = m_Repository.FindById(id);

            m_Repository.Remove(entity);

            return entity == null
                       ? null
                       : ToEntityForResponse(entity);
        }

        internal static IBestTrail ToEntity([NotNull] IBestTrailForResponse response)
        {
            var entity = new DataAccess.BestTrail
                         {
                             Alpha = response.Alpha,
                             BestTrailId = response.BestTrailId,
                             Beta = response.Beta,
                             ColonyId = response.ColonyId,
                             Gamma = response.Gamma,
                             Iteration = response.Iteration,
                             Length = response.Length,
                             Trail = response.Trail,
                             Type = response.Type
                         };

            return entity;
        }

        internal static BestTrailForResponse ToEntityForResponse(IBestTrail entity)
        {
            return new BestTrailForResponse
                   {
                       Alpha = entity.Alpha,
                       BestTrailId = entity.BestTrailId,
                       Beta = entity.Beta,
                       ColonyId = entity.ColonyId,
                       Gamma = entity.Gamma,
                       Iteration = entity.Iteration,
                       Length = entity.Length,
                       Trail = entity.Trail,
                       Type = entity.Type
                   };
        }

        internal static IEnumerable <BestTrailForResponse> ToResponses(IEnumerable <IBestTrail> all)
        {
            BestTrailForResponse[] list = all.Select(ToEntityForResponse)
                                             .ToArray();
            return list;
        }
    }
}