using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonySettings.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonySettingsInformationFinder : IColonySettingsInformationFinder
    {
        public ColonySettingsInformationFinder([NotNull] IColonySettingsRepository repository)
        {
            m_Repository = repository;
        }

        private readonly IColonySettingsRepository m_Repository;

        public IColonySettingsForResponse FindById(Guid id)
        {
            IColonySettings colony = m_Repository.FindById(id);

            return colony == null
                       ? null
                       : ToResponse(colony);
        }

        public IEnumerable <IColonySettingsForResponse> List()
        {
            IEnumerable <IColonySettings> all = m_Repository.All;

            IEnumerable <ColonySettingsForResponse> items = ToResponses(all);

            return items;
        }

        public IColonySettingsForResponse Save(IColonySettingsForResponse colonySettings)
        {
            IColonySettings toBeUpdated = ToEntity(colonySettings);

            m_Repository.Save(toBeUpdated);

            return new ColonySettingsForResponse
                   {
                       CostMatrix = toBeUpdated.CostMatrix,
                       CostPerFeature = toBeUpdated.CostPerFeature,
                       FixedStartNode = toBeUpdated.FixedStartNode,
                       IsFixedStartNode = toBeUpdated.IsFixedStartNode,
                       ColonyId = toBeUpdated.ColonyId,
                       ColonySettingsId = toBeUpdated.ColonySettingsId
                   };
        }

        public IColonySettingsForResponse Delete(Guid id)
        {
            IColonySettings colony = m_Repository.FindById(id);

            m_Repository.Remove(colony);

            return colony == null
                       ? null
                       : ToResponse(colony);
        }

        internal static IColonySettings ToEntity(IColonySettingsForResponse response)
        {
            var settings = new DataAccess.ColonySettings
                           {
                               CostMatrix = response.CostMatrix,
                               CostPerFeature = response.CostPerFeature,
                               FixedStartNode = response.FixedStartNode,
                               IsFixedStartNode = response.IsFixedStartNode,
                               ColonyId = response.ColonyId,
                               ColonySettingsId = response.ColonySettingsId
                           };

            return settings;
        }

        internal static ColonySettingsForResponse ToResponse(IColonySettings settings)
        {
            return new ColonySettingsForResponse
                   {
                       CostMatrix = settings.CostMatrix,
                       CostPerFeature = settings.CostPerFeature,
                       FixedStartNode = settings.FixedStartNode,
                       IsFixedStartNode = settings.IsFixedStartNode,
                       ColonyId = settings.ColonyId,
                       ColonySettingsId = settings.ColonySettingsId
                   };
        }

        internal static IEnumerable <ColonySettingsForResponse> ToResponses(IEnumerable <IColonySettings> all)
        {
            ColonySettingsForResponse[] list = all.Select(ToResponse)
                                                  .ToArray();
            return list;
        }
    }
}