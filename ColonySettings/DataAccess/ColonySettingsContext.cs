using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess;
using Selkie.Web.MicroServices.Common;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonySettings.DataAccess
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class ColonySettingsContext
        : SelkieBaseContext <IColonySettings, ColonySettings>,
          IColonySettingsContext
    {
    }
}