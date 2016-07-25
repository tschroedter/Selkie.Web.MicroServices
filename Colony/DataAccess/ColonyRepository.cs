using JetBrains.Annotations;
using Selkie.Web.MicroServices.Colony.Interfaces.DataAccess;
using Selkie.Web.MicroServices.Common;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.Colony.DataAccess
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonyRepository
        : SelkieBaseRepository <IColony, IColonyContext>,
          IColonyRepository
    {
        public ColonyRepository([NotNull] IColonyContext context)
            : base(context)
        {
        }
    }
}