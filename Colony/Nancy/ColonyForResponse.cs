using System;
using System.Diagnostics.CodeAnalysis;
using Selkie.Web.MicroServices.Colony.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.Colony.Nancy
{
    [ExcludeFromCodeCoverage]
    public sealed class ColonyForResponse : IColonyForResponse
    {
        public ColonyForResponse()
        {
            ColonyId = Guid.Empty;
            Description = string.Empty;
            Status = "Unknown";
        }

        public Guid ColonyId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public Guid Id
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