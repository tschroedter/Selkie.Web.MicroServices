using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Selkie.EasyNetQ;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor
{
    [ExcludeFromCodeCoverage]
    public sealed class Installer : BaseInstaller <Installer>
    {
        public override string GetPrefixOfDllsToInstall()
        {
            return "Selkie.Web.MicroServices.ColonyMonitor";
        }

        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            base.InstallComponents(container,
                                   store);

            Assembly assembly = typeof( Installer ).Assembly;

            var consumers = container.Resolve <IRegisterMessageHandlers>();
            consumers.Register(container,
                               assembly);
            container.Release(consumers);
        }
    }
}