using System;

namespace Selkie.Web.MicroServices.ColonyMonitor.Dtos
{
    public sealed class ColonySettingsDto
    {
        public int[][] CostMatrix { get; set; }
        public int[] CostPerFeature { get; set; }
        public int FixedStartNode { get; set; }
        public bool IsFixedStartNode { get; set; }
        public Guid ColonyId { get; set; }
        public Guid ColonySettingsId { get; set; }
    }
}