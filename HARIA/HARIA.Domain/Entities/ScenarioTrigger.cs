using System;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Enums;

namespace HARIA.Domain.Entities
{
    public class ScenarioTrigger : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ScenarioTriggerType Type { get; set; }

        public DateTime InitialTime { get; set; }

        public DateTime FinalTime { get; set; }

        public ExternalSensor External { get; set; }
    }
}