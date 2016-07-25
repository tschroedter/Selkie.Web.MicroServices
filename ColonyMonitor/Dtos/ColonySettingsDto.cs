namespace Selkie.MicroServices.ColonyMonitor.Dtos
{
    public sealed class ColonySettingsDto
    {
        public int[][] CostMatrix { get; set; }
        public int[] CostPerFeature { get; set; }
        public int FixedStartNode { get; set; }
        public bool IsFixedStartNode { get; set; }
        public int ColonyId { get; set; }
        public int ColonySettingsId { get; set; }
    }
}