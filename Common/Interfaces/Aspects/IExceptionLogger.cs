using System;
using Castle.DynamicProxy;

namespace Selkie.Web.MicroServices.Common.Interfaces.Aspects
{
    public interface IExceptionLogger
    {
        void LogException(IInvocation invocation,
                          Exception exception);
    }
}