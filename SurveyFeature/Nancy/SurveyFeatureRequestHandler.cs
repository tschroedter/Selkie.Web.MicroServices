using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Nancy;
using Newtonsoft.Json;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.SurveyFeature.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SurveyFeatureRequestHandler : ISurveyFeatureRequestHandler
    {
        public SurveyFeatureRequestHandler([NotNull] ISurveyFeatureInformationFinder colonyInformationFinder)
        {
            m_ColonyInformationFinder = colonyInformationFinder;
        }

        private readonly ISurveyFeatureInformationFinder m_ColonyInformationFinder;

        public Response List()
        {
            IEnumerable <ISurveyFeatureForResponse> responses = m_ColonyInformationFinder.List();

            return AsJson(responses);
        }

        public Response FindById(Guid id)
        {
            ISurveyFeatureForResponse response = m_ColonyInformationFinder.FindById(id);

            return response == null
                       ? HttpStatusCode.NotFound
                       : AsJson(response);
        }

        public Response Save(ISurveyFeatureForResponse response)
        {
            ISurveyFeatureForResponse saved = m_ColonyInformationFinder.Save(response);

            return AsJson(saved);
        }

        public Response DeleteById(Guid id)
        {
            ISurveyFeatureForResponse response = m_ColonyInformationFinder.Delete(id);

            return response == null
                       ? HttpStatusCode.NotFound
                       : AsJson(response);
        }

        private Response AsJson(object instance)
        {
            Response response = JsonConvert.SerializeObject(instance);

            response.ContentType = "application/json";

            return response;
        }
    }
}