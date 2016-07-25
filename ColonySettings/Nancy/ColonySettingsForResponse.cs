using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.ColonySettings.Nancy
{
    [ExcludeFromCodeCoverage]
    public sealed class ColonySettingsForResponse : IColonySettingsForResponse
    {
        public ColonySettingsForResponse()
        {
            CostMatrix = new int[0][];
            CostPerFeature = new int[0];
            IsFixedStartNode = false;
        }

        public int Id
        {
            get
            {
                return ColonySettingsId;
            }
            set
            {
                ColonySettingsId = value;
            }
        }

        public int[][] CostMatrix { get; set; }
        public int[] CostPerFeature { get; set; }
        public int FixedStartNode { get; set; }
        public bool IsFixedStartNode { get; set; }
        public int ColonyId { get; set; }
        public int ColonySettingsId { get; set; }
    }
}