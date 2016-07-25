using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.BestTrail.Interfaces.DataAccess;
using Selkie.Web.MicroServices.Common;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.BestTrail.DataAccess
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class BestTrailContext
        : SelkieBaseContext <IBestTrail, BestTrail>,
          IBestTrailContext
    {
    }
}