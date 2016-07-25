using Castle.Windsor;
using Selkie.MicroServices.ColonyMonitor.Dtos;
using Selkie.MicroServices.ColonyMonitor.Interfaces.Handlers;
using Selkie.Services.Aco.Common.Messages;

namespace Selkie.MicroServices.ColonyMonitor.Console
{
    class Program
    {
        private static WindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();
            var installer = new Installer();

            container.Install(installer);
            return container;
        }

        static void Main()
        {
            WindsorContainer container = CreateContainer();

            var testMessage = new CreateColonyMessage
                              {
                                  CostMatrix = new[]
                                               {
                                                   new[]
                                                   {
                                                       1,
                                                       2
                                                   },
                                                   new[]
                                                   {
                                                       3,
                                                       4
                                                   }
                                               },
                                  CostPerFeature = new[]
                                                   {
                                                       1,
                                                       2
                                                   },
                                  FixedStartNode = 1,
                                  IsFixedStartNode = true
                              };

            var colonyManager = container.Resolve <IColonyManager>();
            ColonyDto colony = colonyManager.Create(testMessage);

            var colonySettingsManager = container.Resolve <IColonySettingsManager>();
            colonySettingsManager.Create(testMessage,
                                         colony.ColonyId);

            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadLine();
        }
    }
}