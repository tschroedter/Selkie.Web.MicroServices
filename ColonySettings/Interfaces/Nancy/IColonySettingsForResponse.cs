using Selkie.Web.MicroServices.Common.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy
{
    public interface IColonySettingsForResponse : IResponse
    {
        int[][] CostMatrix { get; set; }
        int[] CostPerFeature { get; set; }
        int FixedStartNode { get; set; }
        bool IsFixedStartNode { get; set; }
        int ColonyId { get; set; }
        int ColonySettingsId { get; set; }
    }
}