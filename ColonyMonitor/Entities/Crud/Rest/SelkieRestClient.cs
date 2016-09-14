using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Newtonsoft.Json;
using RestSharp;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud.Rest
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public class SelkieRestClient : ISelkieRestClient
    {
        // todo remove new statements
        public SelkieRestClient(
            [NotNull] string baseUrl,
            [NotNull] string microServiceName)
        {
            BaseUrl = baseUrl;
            MicroServiceName = microServiceName;
        }

        public string MicroServiceName { get; private set; }
        public string BaseUrl { get; private set; }

        public T CreateOrUpdate <T>(T dtoExecute) where T : new()
        {
            IRestClient client = new RestClient(BaseUrl);
            IRestRequest request = new RestRequest(MicroServiceName,
                                                   Method.POST);

            request.AddJsonBody(dtoExecute);

            IRestResponse <T> restResponse = client.Execute <T>(request);

            var dto = JsonConvert.DeserializeObject <T>(restResponse.Content);

            return dto;
        }

        public T Read <T>(Guid guid) where T : new()
        {
            IRestClient client = new RestClient(BaseUrl);
            IRestRequest request = new RestRequest(MicroServiceName + guid,
                                                   Method.GET);

            IRestResponse <T> restResponse = client.Execute <T>(request);

            var dto = JsonConvert.DeserializeObject <T>(restResponse.Content);

            return dto;
        }
    }
}