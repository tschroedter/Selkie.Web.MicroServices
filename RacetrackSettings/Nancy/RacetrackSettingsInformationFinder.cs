using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.DataAccess;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.RacetrackSettings.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class RacetrackSettingsInformationFinder : IRacetrackSettingsInformationFinder
    {
        public RacetrackSettingsInformationFinder([NotNull] IRacetrackSettingsRepository repository)
        {
            m_Repository = repository;
        }

        private readonly IRacetrackSettingsRepository m_Repository;

        public IRacetrackSettingsForResponse FindById(Guid id)
        {
            IRacetrackSettings entity = m_Repository.FindById(id);

            return entity != null
                       ? ToResponse(entity)
                       : null;
        }

        public IEnumerable <IRacetrackSettingsForResponse> List()
        {
            IEnumerable <IRacetrackSettings> all = m_Repository.All;

            IEnumerable <RacetrackSettingsForResponse> items = ToResponses(all);

            return items;
        }

        public IRacetrackSettingsForResponse Save(IRacetrackSettingsForResponse response)
        {
            IRacetrackSettings toBeUpdated = ToEntity(response);

            m_Repository.Save(toBeUpdated);

            return new RacetrackSettingsForResponse
                   {
                       ColonyId = toBeUpdated.ColonyId,
                       IsPortTurnAllowed = toBeUpdated.IsPortTurnAllowed,
                       IsStarboardTurnAllowed = toBeUpdated.IsStarboardTurnAllowed,
                       RacetrackSettingsId = toBeUpdated.RacetrackSettingsId,
                       TurnRadiusForPort = toBeUpdated.TurnRadiusForPort,
                       TurnRadiusForStarboard = toBeUpdated.TurnRadiusForStarboard
                   };
        }

        public IRacetrackSettingsForResponse Delete(Guid id)
        {
            IRacetrackSettings entity = m_Repository.FindById(id);

            m_Repository.Remove(entity);

            return entity == null
                       ? null
                       : ToResponse(entity);
        }

        internal static IRacetrackSettings ToEntity(IRacetrackSettingsForResponse response)
        {
            var entity = new DataAccess.RacetrackSettings
                         {
                             ColonyId = response.ColonyId,
                             IsPortTurnAllowed = response.IsPortTurnAllowed,
                             IsStarboardTurnAllowed = response.IsStarboardTurnAllowed,
                             RacetrackSettingsId = response.RacetrackSettingsId,
                             TurnRadiusForPort = response.TurnRadiusForPort,
                             TurnRadiusForStarboard = response.TurnRadiusForStarboard
                         };

            return entity;
        }

        internal static RacetrackSettingsForResponse ToResponse(IRacetrackSettings entity)
        {
            return new RacetrackSettingsForResponse
                   {
                       ColonyId = entity.ColonyId,
                       IsPortTurnAllowed = entity.IsPortTurnAllowed,
                       IsStarboardTurnAllowed = entity.IsStarboardTurnAllowed,
                       RacetrackSettingsId = entity.RacetrackSettingsId,
                       TurnRadiusForPort = entity.TurnRadiusForPort,
                       TurnRadiusForStarboard = entity.TurnRadiusForStarboard
                   };
        }

        internal static IEnumerable <RacetrackSettingsForResponse> ToResponses(
            IEnumerable <IRacetrackSettings> all)
        {
            RacetrackSettingsForResponse[] list = all.Select(ToResponse)
                                                     .ToArray();
            return list;
        }
    }
}