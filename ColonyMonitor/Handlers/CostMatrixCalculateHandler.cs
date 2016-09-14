using Castle.Core;
using JetBrains.Annotations;
using Selkie.EasyNetQ;
using Selkie.Services.Racetracks.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager;
using Selkie.Web.MicroServices.Common.Aspects;

namespace Selkie.Web.MicroServices.ColonyMonitor.Handlers
{
    [Interceptor(typeof( ExceptionLoggerAspect ))]
    public class CostMatrixCalculateHandler
        : SelkieMessageHandler <CostMatrixCalculateMessage>
    {
        public CostMatrixCalculateHandler(
            [NotNull] IRacetrackSettingsManager racetrackSettingsManager,
            [NotNull] ISurveyFeaturesManager surveyFeaturesManager)
        {
            m_RacetrackSettingsManager = racetrackSettingsManager;
            m_SurveyFeaturesManager = surveyFeaturesManager;
        }

        private readonly IRacetrackSettingsManager m_RacetrackSettingsManager;
        private readonly ISurveyFeaturesManager m_SurveyFeaturesManager;

        public override void Handle([NotNull] CostMatrixCalculateMessage message)
        {
            m_RacetrackSettingsManager.Create(message);
            m_SurveyFeaturesManager.Create(message);
        }
    }
}