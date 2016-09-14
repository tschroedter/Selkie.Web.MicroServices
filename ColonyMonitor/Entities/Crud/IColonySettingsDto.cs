using System;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud
{
    public interface IColonySettingsDto
    {
        string BaseUrl { get; }
        ColonySettingsDto CreateOrUpdate(ColonySettingsDto newCrudColony);
        ColonySettingsDto Read(Guid guid);
    }
}