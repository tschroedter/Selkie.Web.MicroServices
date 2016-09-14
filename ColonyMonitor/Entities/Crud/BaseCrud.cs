using System;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud.Rest;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud
{
    public abstract class BaseCrud <T>
        : IBaseCrud <T>
        where T : new()
    {
        protected BaseCrud(
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory,
            [NotNull] string microServiceName)
        {
            BaseUrl = reader.BaseUrl;
            MicroServiceName = microServiceName;
            m_SelkieRestClient = factory.Create(BaseUrl,
                                                MicroServiceName);
        }

        private readonly ISelkieRestClient m_SelkieRestClient;

        public string MicroServiceName { get; private set; }

        public string BaseUrl { get; private set; }

        public T Read(Guid guid)
        {
            var dto = m_SelkieRestClient.Read <T>(guid);

            return dto;
        }

        public T CreateOrUpdate(T newDto)
        {
            T dto = m_SelkieRestClient.CreateOrUpdate(newDto);

            return dto;
        }
    }
}