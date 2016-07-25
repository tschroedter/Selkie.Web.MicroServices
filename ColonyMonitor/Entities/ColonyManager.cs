using JetBrains.Annotations;
using RestSharp;
using Selkie.MicroServices.ColonyMonitor.Dtos;
using Selkie.MicroServices.ColonyMonitor.Interfaces;
using Selkie.MicroServices.ColonyMonitor.Interfaces.Handlers;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Windsor;

namespace Selkie.MicroServices.ColonyMonitor.Entities
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonyManager : IColonyManager
    {
        public ColonyManager([NotNull] IBaseUrlReader reader)
        {
            BaseUrl = reader.BaseUrl;
        }

        internal const string MicroServiceName = "colonies";

        public string BaseUrl { get; private set; }

        public ColonyDto Create(CreateColonyMessage message)
        {
            var newColony = new ColonyDto
                            {
                                Description = "CreateColonyMessage",
                                Status = ColonyProgress.Status.Creating
                            };

            var client = new RestClient(BaseUrl);

            var request = new RestRequest(MicroServiceName,
                                          Method.POST);

            request.AddJsonBody(newColony);

            IRestResponse <ColonyDto> createdColony = client.Execute <ColonyDto>(request);

            return createdColony.Data;
        }
    }
}