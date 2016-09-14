using System;

namespace Selkie.Web.MicroServices.ColonyMonitor.Dtos
{
    public sealed class RacetrackSettingsDto
    {
        public RacetrackSettingsDto()
        {
            IsPortTurnAllowed = true;
            IsStarboardTurnAllowed = true;
            TurnRadiusForPort = DefaultTurnRadius;
            TurnRadiusForStarboard = DefaultTurnRadius;
        }

        internal const double DefaultTurnRadius = 30.0;

        public Guid ColonyId { get; set; }

        public bool IsPortTurnAllowed { get; set; }

        public bool IsStarboardTurnAllowed { get; set; }

        public Guid RacetrackSettingsId { get; set; }

        public double TurnRadiusForPort { get; set; }

        public double TurnRadiusForStarboard { get; set; }
    }
}