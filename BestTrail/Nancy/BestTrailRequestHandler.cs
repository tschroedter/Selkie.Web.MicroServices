using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Nancy;
using Newtonsoft.Json;
using Selkie.Web.MicroServices.BestTrail.Interfaces.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.BestTrail.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class BestTrailRequestHandler : IBestTrailRequestHandler
    {
        public BestTrailRequestHandler([NotNull] IBestTrailInformationFinder colonyInformationFinder)
        {
            m_ColonyInformationFinder = colonyInformationFinder;
        }

        private readonly IBestTrailInformationFinder m_ColonyInformationFinder;

        public Response List()
        {
            IEnumerable <IBestTrailForResponse> responses = m_ColonyInformationFinder.List();

            return AsJson(responses);
        }

        public Response FindById(Guid id)
        {
            IBestTrailForResponse response = m_ColonyInformationFinder.FindById(id);

            return response == null
                       ? HttpStatusCode.NotFound
                       : AsJson(response);
        }

        public Response Save(IBestTrailForResponse response)
        {
            IBestTrailForResponse saved = m_ColonyInformationFinder.Save(response);

            return AsJson(saved);
        }

        public Response DeleteById(Guid id)
        {
            IBestTrailForResponse response = m_ColonyInformationFinder.Delete(id);

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