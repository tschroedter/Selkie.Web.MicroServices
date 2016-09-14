using JetBrains.Annotations;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud.Rest
{
    public interface ISelkieRestClientFactory : ITypedFactory
    {
        ISelkieRestClient Create([NotNull] string baseUrl,
                                 [NotNull] string microServiceName);

        void Release([NotNull] ISelkieRestClient selkieRestClient);
    }
}