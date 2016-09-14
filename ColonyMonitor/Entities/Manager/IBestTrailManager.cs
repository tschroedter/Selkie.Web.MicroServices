using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager
{
    public interface IBestTrailManager
    {
        BestTrailDto Create(BestTrailMessage message);
    }
}