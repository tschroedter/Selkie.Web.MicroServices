using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.Colony.Interfaces.DataAccess;
using Selkie.Web.MicroServices.Common;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.Colony.DataAccess
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class ColonyContext
        : SelkieBaseContext <IColony, Colony>,
          IColonyContext
    {
    }
}