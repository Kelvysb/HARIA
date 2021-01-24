using System;
using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class ExternalSensor : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public bool Active { get; set; }

        public string Condition { get; set; }

        public string Script { get; set; }

        public DateTime LastStateChange { get; set; }

        public List<Action> Actions { get; set; }

        public List<ScenarioTrigger> ScenarioTriggers { get; set; }
    }
}