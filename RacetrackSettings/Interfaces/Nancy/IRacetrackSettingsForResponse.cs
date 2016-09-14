using System;
using Selkie.Web.MicroServices.Common.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.RacetrackSettings.Interfaces.Nancy
{
    public interface IRacetrackSettingsForResponse : IResponse
    {
        Guid ColonyId { get; set; }
        bool IsPortTurnAllowed { get; set; }
        bool IsStarboardTurnAllowed { get; set; }
        Guid RacetrackSettingsId { get; set; }
        double TurnRadiusForPort { get; set; }
        double TurnRadiusForStarboard { get; set; }
    }
}