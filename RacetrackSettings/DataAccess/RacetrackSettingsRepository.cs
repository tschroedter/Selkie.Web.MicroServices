using JetBrains.Annotations;
using Selkie.Web.MicroServices.Common;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.DataAccess;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.RacetrackSettings.DataAccess
{
    [ProjectComponent(Lifestyle.Transient)]
    public class RacetrackSettingsRepository
        : SelkieBaseRepository <IRacetrackSettings, IRacetrackSettingsContext>,
          IRacetrackSettingsRepository
    {
        public RacetrackSettingsRepository([NotNull] IRacetrackSettingsContext context)
            : base(context)
        {
        }
    }
}