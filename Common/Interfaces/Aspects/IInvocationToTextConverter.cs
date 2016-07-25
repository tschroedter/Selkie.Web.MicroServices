using Castle.DynamicProxy;
using JetBrains.Annotations;

namespace Selkie.Web.MicroServices.Common.Interfaces.Aspects
{
    public interface IInvocationToTextConverter
    {
        string Convert([NotNull] IInvocation invocation);
    }
}