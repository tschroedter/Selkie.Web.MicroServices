using Selkie.Windsor;

namespace Selkie.Web.MicroServices.Common
{
    public sealed class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.Web.MicroServices.Common";
        }
    }
}