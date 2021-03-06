﻿using System;
using JetBrains.Annotations;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;

namespace Selkie.Web.MicroServices.ColonyMonitor.Interfaces.Handlers
{
    public interface IColonyManager
    {
        ColonyDto Create([NotNull] CreateColonyMessage message);

        void UpdateStatus(Guid colonyId,
                          ColonyProgress.Status status);
    }
}