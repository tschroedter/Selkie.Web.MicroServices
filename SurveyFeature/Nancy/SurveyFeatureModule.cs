using JetBrains.Annotations;
using Nancy;
using Nancy.ModelBinding;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.Nancy;

// ReSharper disable ImplicitlyCapturedClosure

namespace Selkie.Web.MicroServices.SurveyFeature.Nancy
{
    public class SurveyFeatureModule
        : NancyModule
    {
        public SurveyFeatureModule([NotNull] ISurveyFeatureRequestHandler handler)
            : base("/surveyfeature") // todo rename to surveyfeatures (s), check others as well
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:int}" ] =
                parameters => handler.FindById(( int ) parameters.id);

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <SurveyFeatureForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <SurveyFeatureForResponse>());

            Delete [ "/{id:int}" ] =
                parameters => handler.DeleteById(( int ) parameters.id);
        }
    }
}