using JetBrains.Annotations;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy;
using Selkie.Web.MicroServices.Common;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonySettings.DataAccess
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonySettingsRepository
        : SelkieBaseRepository <IColonySettings, IColonySettingsContext>,
          IColonySettingsRepository
    {
        public ColonySettingsRepository([NotNull] IColonySettingsContext context)
            : base(context)
        {
        }
    }
}