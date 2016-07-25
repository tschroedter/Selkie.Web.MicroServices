using JetBrains.Annotations;
using RestSharp;

namespace Selkie.MicroServices.ColonyMonitor.Interfaces
{
    public interface ISelkieRestClient
    {
        IRestResponse <T> Execute <T>([NotNull] IRestRequest request) where T : new();
    }
}