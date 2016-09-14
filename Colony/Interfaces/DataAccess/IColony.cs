using System;
using Selkie.Web.MicroServices.Colony.DataAccess;
using Selkie.Web.MicroServices.Common.Interfaces;

namespace Selkie.Web.MicroServices.Colony.Interfaces.DataAccess
{
    public interface IColony : IEntity
    {
        Guid ColonyId { get; set; }
        string Description { get; set; }
        ColonyProgress.Status Status { get; set; }
    }
}