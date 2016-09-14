using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Nancy.Bootstrappers.Windsor;
using Selkie.Windsor;

namespace SelfHosting
{
    [ExcludeFromCodeCoverage]
    public class Bootstrapper : WindsorNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IWindsorContainer container)
        {
            base.ConfigureApplicationContainer(container);

            // Add the Array Resolver, so we can take dependencies on T[]
            // while only registering T.
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Install(FromAssembly.Containing(typeof( Installer )));
            container.Install(FromAssembly.Containing(typeof( Selkie.EasyNetQ.Installer )));
            container.Install(FromAssembly.Containing(typeof( Selkie.Web.MicroServices.Common.Installer )));
            container.Install(FromAssembly.Containing(typeof( Selkie.Web.MicroServices.Colony.Installer )));
            container.Install(FromAssembly.Containing(typeof( Selkie.Web.MicroServices.ColonySettings.Installer )));
            container.Install(FromAssembly.Containing(typeof( Selkie.Web.MicroServices.BestTrail.Installer )));
            container.Install(FromAssembly.Containing(typeof( Selkie.Web.MicroServices.SurveyFeature.Installer )));
            container.Install(FromAssembly.Containing(typeof( Selkie.Web.MicroServices.RacetrackSettings.Installer )));
            container.Install(FromAssembly.Containing(typeof( Selkie.Web.MicroServices.ColonyMonitor.Installer )));
        }
    }
}