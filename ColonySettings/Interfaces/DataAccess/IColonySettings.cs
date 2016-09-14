using System;
using Selkie.Web.MicroServices.Common.Interfaces;

namespace Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess
{
    public interface IColonySettings : IEntity
    {
        int[][] CostMatrix { get; set; }
        int[] CostPerFeature { get; set; }
        int FixedStartNode { get; set; }
        bool IsFixedStartNode { get; set; }
        Guid ColonyId { get; set; }
        Guid ColonySettingsId { get; set; }
    }
}