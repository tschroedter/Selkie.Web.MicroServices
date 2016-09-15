using Castle.Core;
using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces.Handlers;
using Selkie.Web.MicroServices.Common.Aspects;

namespace Selkie.Web.MicroServices.ColonyMonitor.Handlers
{
    [Interceptor(typeof( ExceptionLoggerAspect ))]
    public class StartHandler
        : SelkieMessageHandler <StartMessage>
    {
        // todo and create BaseMessage class with common ColonyId
        // todo create BaseUpdateColonyStatusHandler
        public StartHandler(
            [NotNull] IColonyManager colonyManager)
        {
            m_ColonyManager = colonyManager;
        }

        private readonly IColonyManager m_ColonyManager;

        public override void Handle([NotNull] StartMessage message)
        {
            m_ColonyManager.UpdateStatus(message.ColonyId,
                                         ColonyProgress.Status.Starting);
        }
    }
}