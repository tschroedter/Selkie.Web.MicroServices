using Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess;
using Selkie.Web.MicroServices.Common.Interfaces;

namespace Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy
{
    public interface IColonySettingsRepository : IRepository <IColonySettings>
    {
    }
}