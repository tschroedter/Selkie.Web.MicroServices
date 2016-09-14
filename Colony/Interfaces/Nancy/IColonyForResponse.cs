using System;
using Selkie.Web.MicroServices.Common.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.Colony.Interfaces.Nancy
{
    public interface IColonyForResponse : IResponse
    {
        Guid ColonyId { get; set; }
        string Description { get; set; }
        string Status { get; set; }
    }
}