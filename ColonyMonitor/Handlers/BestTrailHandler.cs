using System;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager;
using Selkie.Web.MicroServices.Common.Aspects;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor.Handlers
{
    [Interceptor(typeof( ExceptionLoggerAspect ))]
    public class BestTrailHandler
        : SelkieMessageHandler <BestTrailMessage>
    {
        public BestTrailHandler(
            [NotNull] ISelkieLogger logger,
            [NotNull] IBestTrailManager manager)
        {
            m_Logger = logger;
            m_Manager = manager;
        }

        private readonly ISelkieLogger m_Logger;
        private readonly IBestTrailManager m_Manager;

        public override void Handle([NotNull] BestTrailMessage message)
        {
            if ( !IsValidMessage(message) )
            {
                return;
            }

            m_Manager.Create(message);
        }

        private bool IsValidMessage(BestTrailMessage message)
        {
            if ( Guid.Empty != message.ColonyId )
            {
                return true;
            }

            m_Logger.Warn("BestTrailMessage has ColonyId set to Guid.Empty!");

            return false;
        }
    }
}