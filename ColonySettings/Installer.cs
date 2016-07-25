using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonySettings
{
    public sealed class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.Web.MicroServices.ColonySettings";
        }
    }
}