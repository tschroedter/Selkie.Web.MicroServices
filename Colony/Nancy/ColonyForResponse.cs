using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.Colony.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.Colony.Nancy
{
    [ExcludeFromCodeCoverage]
    public sealed class ColonyForResponse : IColonyForResponse
    {
        public ColonyForResponse()
        {
            ColonyId = 0;
            Description = string.Empty;
            Status = "Unknown";
        }

        public int ColonyId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public int Id
        {
            get
            {
                return ColonyId;
            }
            set
            {
                ColonyId = value;
            }
        }
    }
}