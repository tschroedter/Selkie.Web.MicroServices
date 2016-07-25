using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.Common;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.DataAccess;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.RacetrackSettings.DataAccess
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class RacetrackSettingsContext
        : SelkieBaseContext <IRacetrackSettings, RacetrackSettings>,
          IRacetrackSettingsContext
    {
    }
}