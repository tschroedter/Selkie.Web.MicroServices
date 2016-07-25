using JetBrains.Annotations;
using Selkie.Web.MicroServices.Common;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.DataAccess;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.SurveyFeature.DataAccess
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SurveyFeatureRepository
        : SelkieBaseRepository <ISurveyFeature, ISurveyFeatureContext>,
          ISurveyFeatureRepository
    {
        public SurveyFeatureRepository([NotNull] ISurveyFeatureContext context)
            : base(context)
        {
        }
    }
}