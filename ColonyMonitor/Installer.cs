using Selkie.Windsor;

namespace Selkie.MicroServices.ColonyMonitor
{
    public sealed class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.MicroServices.ColonyMonitor";
        }
    }
}