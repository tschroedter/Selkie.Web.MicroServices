using System;
using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.RacetrackSettings.Nancy
{
    [ExcludeFromCodeCoverage]
    public sealed class RacetrackSettingsForResponse : IRacetrackSettingsForResponse
    {
        public RacetrackSettingsForResponse()
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

        public Guid Id
        {
            get
            {
                return RacetrackSettingsId;
            }
            set
            {
                RacetrackSettingsId = value;
            }
        }
    }
}