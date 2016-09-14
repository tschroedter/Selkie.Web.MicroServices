using JetBrains.Annotations;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud.Rest;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud
{
    [ProjectComponent(Lifestyle.Transient)]
    public class CrudSurveyFeatureDto
        : BaseCrud <SurveyFeatureDto>,
          ICrudSurveyFeatureDto
    {
        public CrudSurveyFeatureDto(
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory)
            : base(reader,
                   factory,
                   "/surveyfeatures/")
        {
        }
    }
}