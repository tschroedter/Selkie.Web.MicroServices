using System;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.Common.Interfaces.Aspects;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.Common.Aspects
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ExceptionLoggerAspect : IInterceptor
    {
        public ExceptionLoggerAspect([NotNull] IExceptionLogger exceptionLogger)
        {
            m_ExceptionLogger = exceptionLogger;
        }

        private readonly IExceptionLogger m_ExceptionLogger;

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch ( Exception exception )
            {
                m_ExceptionLogger.LogException(invocation,
                                               exception);

                throw;
            }
        }
    }
}