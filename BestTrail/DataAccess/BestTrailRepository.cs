using JetBrains.Annotations;
using Selkie.Web.MicroServices.BestTrail.Interfaces.DataAccess;
using Selkie.Web.MicroServices.Common;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.BestTrail.DataAccess
{
    [ProjectComponent(Lifestyle.Transient)]
    public class BestTrailRepository
        : SelkieBaseRepository <IBestTrail, IBestTrailContext>,
          IBestTrailRepository
    {
        public BestTrailRepository([NotNull] IBestTrailContext context)
            : base(context)
        {
        }
    }
}