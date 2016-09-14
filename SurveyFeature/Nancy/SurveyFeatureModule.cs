using System;
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
            : base("/surveyfeatures")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:Guid}" ] =
                parameters => handler.FindById(( Guid ) parameters.id);

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <SurveyFeatureForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <SurveyFeatureForResponse>());

            Delete [ "/{id:Guid}" ] =
                parameters => handler.DeleteById(( Guid ) parameters.id);
        }
    }
}