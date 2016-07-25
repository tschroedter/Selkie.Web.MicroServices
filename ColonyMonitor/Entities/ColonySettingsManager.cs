using JetBrains.Annotations;
using RestSharp;
using Selkie.MicroServices.ColonyMonitor.Dtos;
using Selkie.MicroServices.ColonyMonitor.Interfaces;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Windsor;

namespace Selkie.MicroServices.ColonyMonitor.Entities
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonySettingsManager
        : IColonySettingsManager
    {
        public ColonySettingsManager(
            [NotNull] ISelkieRestClient client)
        {
            Client = client;
        }

        internal const string MicroServiceName = "colonysettings";

        private ISelkieRestClient Client { get; set; }

        public ColonySettingsDto Create(
            CreateColonyMessage message,
            int colonyId)
        {
            ColonySettingsDto toBeCreatedDto = ConvertMessageToDto(message,
                                                                   colonyId);

            RestRequest request = CreateRestRequest(toBeCreatedDto);

            IRestResponse <ColonySettingsDto> createdDto = Client.Execute <ColonySettingsDto>(request);

            return createdDto.Data;
        }

        private static ColonySettingsDto ConvertMessageToDto(CreateColonyMessage message,
                                                             int colonyId)
        {
            var toBeCreatedDto = new ColonySettingsDto
                                 {
                                     ColonyId = colonyId,
                                     CostMatrix = message.CostMatrix,
                                     CostPerFeature = message.CostPerFeature,
                                     FixedStartNode = message.FixedStartNode,
                                     IsFixedStartNode = message.IsFixedStartNode
                                 };
            return toBeCreatedDto;
        }

        private static RestRequest CreateRestRequest(ColonySettingsDto toBeCreatedDto)
        {
            var request = new RestRequest(MicroServiceName,
                                          Method.POST);

            request.AddJsonBody(toBeCreatedDto);
            return request;
        }
    }
}