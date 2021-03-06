﻿using System;
using JetBrains.Annotations;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Web.MicroServices.ColonyMonitor.Dtos;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces.Handlers;
using Selkie.Windsor;

namespace Selkie.Web.MicroServices.ColonyMonitor.Entities.Manager
{
    [ProjectComponent(Lifestyle.Transient)] // todo logger aspect
    public class ColonyManager : IColonyManager
    {
        public ColonyManager([NotNull] ICrudColonyDto crud)
        {
            m_Crud = crud;
        }

        private readonly ICrudColonyDto m_Crud;

        public ColonyDto Create(CreateColonyMessage message)
        {
            var dto = new ColonyDto
                      {
                          ColonyId = message.ColonyId,
                          Description = "CreateColonyMessage",
                          Status = ColonyProgress.Status.Creating
                      };

            return m_Crud.CreateOrUpdate(dto);
        }

        public void UpdateStatus(
            Guid colonyId,
            ColonyProgress.Status status)
        {
            ColonyDto dto = m_Crud.Read(colonyId);

            dto.Status = status;

            m_Crud.CreateOrUpdate(dto);
        }

        public void Created(Guid colonyId)
        {
            UpdateStatus(colonyId,
                         ColonyProgress.Status.Created);
        }
    }
}