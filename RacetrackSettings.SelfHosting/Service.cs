using System.Diagnostics.CodeAnalysis;

namespace Selkie.Web.MicroServices.RacetrackSettings.SelfHosting
{
    [ExcludeFromCodeCoverage]
    public class Service
    {
        public bool Start()
        {
            return true;
        }

        public bool Stop()
        {
            return true;
        }
    }
}