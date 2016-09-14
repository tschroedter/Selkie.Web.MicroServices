using System;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud.Rest
{
    public interface ISelkieRestClient
    {
        string MicroServiceName { get; }
        string BaseUrl { get; }
        T CreateOrUpdate <T>(T dtoExecute) where T : new();
        T Read <T>(Guid guid) where T : new();
    }
}