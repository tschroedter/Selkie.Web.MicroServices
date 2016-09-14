using System;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud
{
    public interface IBaseCrud <T>
        where T : new()
    {
        string MicroServiceName { get; }
        string BaseUrl { get; }
        T CreateOrUpdate(T newDto);
        T Read(Guid guid);
    }
}