using System.Collections.Generic;
using JetBrains.Annotations;
using Nancy;
using Newtonsoft.Json;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.RacetrackSettings.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class RacetrackSettingsRequestHandler : IRacetrackSettingsRequestHandler
    {
        public RacetrackSettingsRequestHandler([NotNull] IRacetrackSettingsInformationFinder colonyInformationFinder)
        {
            m_ColonyInformationFinder = colonyInformationFinder;
        }

        private readonly IRacetrackSettingsInformationFinder m_ColonyInformationFinder;

        public Response List()
        {
            IEnumerable <IRacetrackSettingsForResponse> responses = m_ColonyInformationFinder.List();

            return AsJson(responses);
        }

        public Response FindById(int id)
        {
            IRacetrackSettingsForResponse response = m_ColonyInformationFinder.FindById(id);

            return response == null
                       ? HttpStatusCode.NotFound
                       : AsJson(response);
        }

        public Response Save(IRacetrackSettingsForResponse response)
        {
            IRacetrackSettingsForResponse saved = m_ColonyInformationFinder.Save(response);

            return AsJson(saved);
        }

        public Response DeleteById(int id)
        {
            IRacetrackSettingsForResponse response = m_ColonyInformationFinder.Delete(id);

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