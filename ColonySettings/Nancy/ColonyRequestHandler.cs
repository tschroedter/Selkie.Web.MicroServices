using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Nancy;
using Newtonsoft.Json;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonySettings.Nancy
{
    [ProjectComponent(Lifestyle.Transient)]
    public class ColonyRequestHandler : IColonyRequestHandler
    {
        public ColonyRequestHandler([NotNull] IColonySettingsInformationFinder colonySettingsInformationFinder)
        {
            m_ColonySettingsInformationFinder = colonySettingsInformationFinder;
        }

        private readonly IColonySettingsInformationFinder m_ColonySettingsInformationFinder;

        public Response List()
        {
            IEnumerable <IColonySettingsForResponse> responses = m_ColonySettingsInformationFinder.List();

            return AsJson(responses);
        }

        public Response FindById(Guid id)
        {
            IColonySettingsForResponse response = m_ColonySettingsInformationFinder.FindById(id);

            return response == null
                       ? HttpStatusCode.NotFound
                       : AsJson(response);
        }

        public Response Save(IColonySettingsForResponse response)
        {
            IColonySettingsForResponse saved = m_ColonySettingsInformationFinder.Save(response);

            return AsJson(saved);
        }

        public Response DeleteById(Guid id)
        {
            IColonySettingsForResponse response = m_ColonySettingsInformationFinder.Delete(id);

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