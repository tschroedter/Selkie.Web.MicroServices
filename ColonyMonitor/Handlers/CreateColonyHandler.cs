using Castle.Core;
using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.MicroServices.ColonyMonitor.Interfaces.Handlers;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.Common.Aspects;

namespace Selkie.MicroServices.ColonyMonitor.Handlers
{
    [Interceptor(typeof( ExceptionLoggerAspect ))] // todo add ExceptionLoggerAspect to Selkie.AOP
    public class CreateColonyHandler
        : SelkieMessageHandler <CreateColonyMessage>
    {
        public CreateColonyHandler([NotNull] IColonyManager colonyManager)
        {
            m_ColonyManager = colonyManager;
        }

        private readonly IColonyManager m_ColonyManager;

        public override void Handle([NotNull] CreateColonyMessage message)
        {
            m_ColonyManager.Create(message);
        }
    }
}