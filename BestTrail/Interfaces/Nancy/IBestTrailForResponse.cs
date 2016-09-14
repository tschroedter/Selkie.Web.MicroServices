using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.Common.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.BestTrail.Interfaces.Nancy
{
    public interface IBestTrailForResponse : IResponse
    {
        double Alpha { get; set; }
        Guid BestTrailId { get; set; }
        double Beta { get; set; }
        Guid ColonyId { get; set; }
        double Gamma { get; set; }
        int Iteration { get; set; }
        double Length { get; set; }

        [NotNull]
        IEnumerable <int> Trail { get; set; }

        string Type { get; set; }
    }
}