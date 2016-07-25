namespace Selkie.MicroServices.ColonyMonitor.Dtos
{
    public sealed class ColonyDto
    {
        public ColonyDto() // todo need GUID in message
        {
            Description = string.Empty;
            Status = ColonyProgress.Status.Unknown;
        }

        public int ColonyId { get; set; }

        public string Description { get; set; }

        public ColonyProgress.Status Status { get; set; }
    }
}