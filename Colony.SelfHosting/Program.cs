using System;
using System.Diagnostics.CodeAnalysis;
using NLog;

namespace Selkie.Web.MicroServices.Colony.SelfHosting
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static void Main()
        {
            try
            {
                var reader = new PortnumberReader(Logger);
                var host = new Host(Logger,
                                    reader.Portnumber);

                host.CreateHost();
            }
            catch ( Exception exception )
            {
                Logger.Error(exception);
            }
        }
    }
}