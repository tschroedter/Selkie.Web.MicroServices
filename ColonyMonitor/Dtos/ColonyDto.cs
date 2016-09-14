using System;

namespace Selkie.Web.MicroServices.ColonyMonitor.Dtos
{
    public sealed class ColonyDto // todo should not duplicate here, instead use reference Colony.Common assembly
    {
        public ColonyDto()
        {
            ColonyId = Guid.Empty;
            Description = string.Empty;
            Status = ColonyProgress.Status.Unknown;
        }

        public Guid ColonyId { get; set; }

        public string Description { get; set; }

        public ColonyProgress.Status Status { get; set; }
    }
}