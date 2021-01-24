﻿using System;
using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class Sensor : IEntity
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public int AmbientId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public int ActiveLowerBound { get; set; }

        public int ActiveUpperBound { get; set; }

        public bool Active { get; set; }

        public DateTime LastStateChange { get; set; }

        public List<Action> Actions { get; set; }

        public List<ScenarioTrigger> ScenarioTriggers { get; set; }
    }
}