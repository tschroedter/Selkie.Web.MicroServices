using Selkie.Web.MicroServices.Common.Interfaces;

namespace Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess
{
    public interface IColonySettings : IEntity
    {
        int[][] CostMatrix { get; set; }
        int[] CostPerFeature { get; set; }
        int FixedStartNode { get; set; }
        bool IsFixedStartNode { get; set; }
        int ColonyId { get; set; }
        int ColonySettingsId { get; set; }
    }
}