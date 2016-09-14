using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Singleton)]
    public class BaseUrlReader : IBaseUrlReader
    {
        public BaseUrlReader([NotNull] ISelkieLogger logger)
        {
            logger.Info("Trying to read 'BaseUrl' from app.config file...");

            string portnumberText = ConfigurationManager.AppSettings [ "BaseUrl" ];

            BaseUrl = portnumberText;

            logger.Info("Found 'BaseUrl' in app.config file and using: {0}",
                        BaseUrl);
        }

        public string BaseUrl { get; private set; }
    }
}