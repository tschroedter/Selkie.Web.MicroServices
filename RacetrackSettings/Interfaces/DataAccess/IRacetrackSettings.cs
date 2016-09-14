using System;
using Selkie.Web.MicroServices.Common.Interfaces;

namespace Selkie.Web.MicroServices.RacetrackSettings.Interfaces.DataAccess
{
    public interface IRacetrackSettings : IEntity
    {
        Guid ColonyId { get; set; }
        bool IsPortTurnAllowed { get; set; }
        bool IsStarboardTurnAllowed { get; set; }
        Guid RacetrackSettingsId { get; set; }
        double TurnRadiusForPort { get; set; }
        double TurnRadiusForStarboard { get; set; }
    }
}