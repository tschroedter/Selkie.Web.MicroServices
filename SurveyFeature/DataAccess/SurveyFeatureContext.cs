using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.Common;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.DataAccess;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.SurveyFeature.DataAccess
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class SurveyFeatureContext
        : SelkieBaseContext <ISurveyFeature, SurveyFeature>,
          ISurveyFeatureContext
    {
    }
}