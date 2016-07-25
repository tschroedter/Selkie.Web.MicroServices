using JetBrains.Annotations;
using Selkie.MicroServices.ColonyMonitor.Dtos;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.MicroServices.ColonyMonitor.Interfaces.Handlers
{
    public interface IColonyManager
    {
        ColonyDto Create([NotNull] CreateColonyMessage message);
    }
}