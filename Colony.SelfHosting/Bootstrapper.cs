using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Nancy.Bootstrappers.Windsor;

namespace Selkie.Web.MicroServices.Colony.SelfHosting
{
    [ExcludeFromCodeCoverage]
    public class Bootstrapper : WindsorNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IWindsorContainer existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);

            // Add the Array Resolver, so we can take dependencies on T[]
            // while only registering T.
            existingContainer.Kernel.Resolver.AddSubResolver(new ArrayResolver(existingContainer.Kernel));

            existingContainer.Install(FromAssembly.Containing(typeof( Windsor.Installer )));
            existingContainer.Install(FromAssembly.Containing(typeof( Common.Installer )));
            existingContainer.Install(FromAssembly.Containing(typeof( Installer )));
        }
    }
}