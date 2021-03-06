﻿using JetBrains.Annotations;
using Selkie.Services.Racetracks.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager
{
    public interface IRacetrackSettingsManager
    {
        RacetrackSettingsDto Create([NotNull] CostMatrixCalculateMessage message);
    }
}