using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;

namespace Selkie.MicroServices.ColonyMonitor.Console
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static WindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();
            var installer = new Installer();

            container.Install(installer);
            return container;
        }

        private static void Main()
        {
            WindsorContainer container = CreateContainer();

            System.Console.WriteLine("Create container {0}...",
                                     container.Name);

            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadLine();
        }
    }
}