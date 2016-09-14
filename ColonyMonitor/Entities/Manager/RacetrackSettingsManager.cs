using JetBrains.Annotations;
using Selkie.Services.Racetracks.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager
{
    [ProjectComponent(Lifestyle.Transient)] // todo logger aspect
    public class RacetrackSettingsManager : IRacetrackSettingsManager
    {
        public RacetrackSettingsManager([NotNull] ICrudRacetrackSettingsDto crud)
        {
            m_Crud = crud;
        }

        private readonly ICrudRacetrackSettingsDto m_Crud;

        public RacetrackSettingsDto Create(CostMatrixCalculateMessage message)
        {
            var dto = new RacetrackSettingsDto
                      {
                          ColonyId = message.ColonyId,
                          IsPortTurnAllowed = message.IsPortTurnAllowed,
                          IsStarboardTurnAllowed = message.IsStarboardTurnAllowed,
                          TurnRadiusForPort = message.TurnRadiusForPort,
                          TurnRadiusForStarboard = message.TurnRadiusForStarboard
                      };

            return m_Crud.CreateOrUpdate(dto);
        }
    }
}