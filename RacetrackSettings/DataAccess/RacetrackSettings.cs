using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.RacetrackSettings.DataAccess
{
    [ExcludeFromCodeCoverage]
    public sealed class RacetrackSettings : IRacetrackSettings
    {
        public RacetrackSettings()
        {
            IsPortTurnAllowed = true;
            IsStarboardTurnAllowed = true;
            TurnRadiusForPort = DefaultTurnRadius;
            TurnRadiusForStarboard = DefaultTurnRadius;
        }

        internal const double DefaultTurnRadius = 30.0;

        [Required]
        public int ColonyId { get; set; }

        [Required]
        public bool IsPortTurnAllowed { get; set; }

        [Required]
        public bool IsStarboardTurnAllowed { get; set; }

        [Key]
        public int RacetrackSettingsId { get; set; }

        [Required]
        public double TurnRadiusForPort { get; set; }

        [Required]
        public double TurnRadiusForStarboard { get; set; }

        [NotMapped]
        public int Id
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