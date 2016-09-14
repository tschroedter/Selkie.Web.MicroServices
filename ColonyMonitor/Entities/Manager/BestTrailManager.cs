using System.Linq;
using JetBrains.Annotations;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager
{
    [ProjectComponent(Lifestyle.Transient)] // todo logger aspect
    public class BestTrailManager : IBestTrailManager
    {
        public BestTrailManager([NotNull] ICrudBestTrailDto crud)
        {
            m_Crud = crud;
        }

        private readonly ICrudBestTrailDto m_Crud;

        public BestTrailDto Create(BestTrailMessage message)
        {
            var dto = new BestTrailDto
                      {
                          Alpha = message.Alpha,
                          Beta = message.Beta,
                          ColonyId = message.ColonyId,
                          Gamma = message.Gamma,
                          Iteration = message.Iteration,
                          Length = message.Length,
                          Trail = message.Trail.ToArray(),
                          Type = message.Type
                      };

            return m_Crud.CreateOrUpdate(dto);
        }
    }
}