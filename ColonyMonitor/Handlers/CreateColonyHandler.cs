using Castle.Core;
using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces.Handlers;
using Selkie.Web.MicroServices.Common.Aspects;

namespace Selkie.Web.MicroServices.ColonyMonitor.Handlers
{
    [Interceptor(typeof( ExceptionLoggerAspect ))]
    public class CreateColonyHandler
        : SelkieMessageHandler <CreateColonyMessage>
    {
        public CreateColonyHandler(
            [NotNull] IColonyManager colonyManager,
            [NotNull] IColonySettingsManager colonySettingsManager)
        {
            m_ColonyManager = colonyManager;
            m_ColonySettingsManager = colonySettingsManager;
        }

        private readonly IColonyManager m_ColonyManager;
        private readonly IColonySettingsManager m_ColonySettingsManager;

        public override void Handle([NotNull] CreateColonyMessage message)
        {
            m_ColonyManager.Create(message);
            m_ColonySettingsManager.Create(message);
        }
    }
}