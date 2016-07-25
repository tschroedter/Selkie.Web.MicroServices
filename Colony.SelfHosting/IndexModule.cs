using System.Diagnostics.CodeAnalysis;
using Nancy;

namespace Selkie.Web.MicroServices.Colony.SelfHosting
{
    [ExcludeFromCodeCoverage]
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get [ "/" ] = parameters => View [ "index" ];
        }
    }
}