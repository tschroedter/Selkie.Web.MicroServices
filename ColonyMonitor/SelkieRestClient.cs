using JetBrains.Annotations;
using RestSharp;
using Selkie.MicroServices.ColonyMonitor.Interfaces;
using Selkie.Windsor;

namespace Selkie.MicroServices.ColonyMonitor
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SelkieRestClient : ISelkieRestClient
    {
        public SelkieRestClient([NotNull] IBaseUrlReader reader)
        {
            Client = new RestClient(reader.BaseUrl);
        }

        private IRestClient Client { get; set; }

        public IRestResponse <T> Execute <T>(IRestRequest request) where T : new()
        {
            return Client.Execute <T>(request);
        }
    }
}