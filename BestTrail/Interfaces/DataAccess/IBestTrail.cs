using System;
using System.Collections.Generic;
using Selkie.Web.MicroServices.Common.Interfaces;

namespace Selkie.Web.MicroServices.BestTrail.Interfaces.DataAccess
{
    public interface IBestTrail : IEntity
    {
        double Alpha { get; set; }
        Guid BestTrailId { get; set; }
        double Beta { get; set; }
        Guid ColonyId { get; set; }
        double Gamma { get; set; }
        int Iteration { get; set; }
        double Length { get; set; }
        IEnumerable <int> Trail { get; set; }
        string Type { get; set; }
    }
}