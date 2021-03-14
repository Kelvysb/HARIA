﻿using System;
using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class ActuatorEntity : IEntity
    {
        public int Id { get; set; }

        public int NodeId { get; set; }

        public NodeEntity Node { get; set; }

        public int AmbientId { get; set; }

        public AmbientEntity Ambient { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public bool LockState { get; set; }

        public string Message { get; set; }

        public int DefaultActiveTime { get; set; }

        public DateTime? DeactivationTime { get; set; }

        public DateTime LastStateChange { get; set; }

        public List<ActionEventEntity> Actions { get; set; }
    }
}