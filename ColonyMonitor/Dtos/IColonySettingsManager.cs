using Selkie.Services.Aco.Common.Messages;

namespace Selkie.MicroServices.ColonyMonitor.Dtos
{
    public interface IColonySettingsManager
    {
        ColonySettingsDto Create(CreateColonyMessage message,
                                 int colonyId);
    }
}