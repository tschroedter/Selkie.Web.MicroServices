using JetBrains.Annotations;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonySettingsManager
        : IColonySettingsManager
    {
        public ColonySettingsManager(
            [NotNull] ICrudColonySettingsDto crud)
        {
            m_Crud = crud;
        }

        private readonly ICrudColonySettingsDto m_Crud;

        public ColonySettingsDto Create(CreateColonyMessage message)
        {
            var dto = new ColonySettingsDto
                      {
                          ColonyId = message.ColonyId,
                          CostMatrix = message.CostMatrix,
                          CostPerFeature = message.CostPerFeature,
                          FixedStartNode = message.FixedStartNode,
                          IsFixedStartNode = message.IsFixedStartNode
                      };

            return m_Crud.CreateOrUpdate(dto);
        }
    }
}