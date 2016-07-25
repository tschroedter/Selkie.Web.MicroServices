using System.Collections.Generic;
using JetBrains.Annotations;
using Nancy;
using Newtonsoft.Json;
using Selkie.Web.MicroServices.Colony.Interfaces.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.Colony.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonyRequestHandler : IColonyRequestHandler
    {
        public ColonyRequestHandler([NotNull] IColonyInformationFinder colonyInformationFinder)
        {
            m_ColonyInformationFinder = colonyInformationFinder;
        }

        private readonly IColonyInformationFinder m_ColonyInformationFinder;

        public Response List()
        {
            IEnumerable <IColonyForResponse> responses = m_ColonyInformationFinder.List();

            return AsJson(responses);
        }

        public Response FindById(int id)
        {
            IColonyForResponse response = m_ColonyInformationFinder.FindById(id);

            return response == null
                       ? HttpStatusCode.NotFound
                       : AsJson(response);
        }

        public Response Save(IColonyForResponse response)
        {
            IColonyForResponse saved = m_ColonyInformationFinder.Save(response);

            return AsJson(saved);
        }

        public Response DeleteById(int id)
        {
            IColonyForResponse response = m_ColonyInformationFinder.Delete(id);

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