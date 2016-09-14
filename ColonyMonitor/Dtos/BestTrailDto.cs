using System;

namespace Selkie.Web.MicroServices.ColonyMonitor.Dtos
{
    public sealed class BestTrailDto // todo should not duplicate here, instead use reference Colony.Common assembly
    {
        public BestTrailDto()
        {
            Trail = new int[0];
        }

        public double Alpha { get; set; }
        public Guid BestTrailId { get; set; }
        public double Beta { get; set; }
        public Guid ColonyId { get; set; }
        public double Gamma { get; set; }
        public int Iteration { get; set; }
        public double Length { get; set; }
        public int[] Trail { get; set; }
        public string Type { get; set; }
    }
}