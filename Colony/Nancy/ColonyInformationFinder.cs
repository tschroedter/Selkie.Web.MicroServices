using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.Colony.DataAccess;
using Selkie.Web.MicroServices.Colony.Interfaces.DataAccess;
using Selkie.Web.MicroServices.Colony.Interfaces.Nancy;
using Selkie.Windsor;
using Selkie.Windsor.Extensions;

namespace Selkie.Web.MicroServices.Colony.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonyInformationFinder : IColonyInformationFinder
    {
        public ColonyInformationFinder(
            [NotNull] ISelkieLogger logger,
            [NotNull] IColonyRepository repository)
        {
            m_Logger = logger;
            m_Repository = repository;
        }

        private readonly ISelkieLogger m_Logger;
        private readonly IColonyRepository m_Repository;

        public IColonyForResponse FindById(int id)
        {
            IColony colony = m_Repository.FindById(id);

            return colony != null
                       ? ToEntityForResponse(colony)
                       : null;
        }

        public IEnumerable <IColonyForResponse> List()
        {
            IEnumerable <IColony> all = m_Repository.All;

            IEnumerable <ColonyForResponse> items = ToResponses(all);

            return items;
        }

        public IColonyForResponse Save(IColonyForResponse colony)
        {
            IColony toBeUpdated = ToEntity(colony);

            m_Repository.Save(toBeUpdated);

            return new ColonyForResponse
                   {
                       ColonyId = toBeUpdated.Id,
                       Description = toBeUpdated.Description,
                       Status = toBeUpdated.Status.ToString()
                   };
        }

        public IColonyForResponse Delete(int id)
        {
            IColony colony = m_Repository.FindById(id);

            m_Repository.Remove(colony);

            return colony == null
                       ? null
                       : ToEntityForResponse(colony);
        }

        internal static ColonyForResponse ToEntityForResponse(IColony colony)
        {
            return new ColonyForResponse
                   {
                       ColonyId = colony.ColonyId,
                       Description = colony.Description,
                       Status = colony.Status.ToString()
                   };
        }

        internal static IEnumerable <ColonyForResponse> ToResponses(IEnumerable <IColony> all)
        {
            ColonyForResponse[] list = all.Select(ToEntityForResponse)
                                          .ToArray();
            return list;
        }

        internal ColonyProgress.Status ConvertStringToStatus([NotNull] string statusAsText)
        {
            ColonyProgress.Status status;

            if ( Enum.TryParse(statusAsText,
                               out status) )
            {
                return status;
            }

            m_Logger.Warn("'{0}' is not a known colony status! - Using default status 'Unknown'".Inject(statusAsText));

            return ColonyProgress.Status.Unknown;
        }

        internal IColony ToEntity([NotNull] IColonyForResponse colonyForResponse)
        {
            ColonyProgress.Status status = ConvertStringToStatus(colonyForResponse.Status);

            var colony = new DataAccess.Colony
                         {
                             ColonyId = colonyForResponse.ColonyId,
                             Description = colonyForResponse.Description,
                             Status = status
                         };

            return colony;
        }
    }
}