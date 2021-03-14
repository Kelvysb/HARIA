using System;
using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Enums;

namespace HARIA.Domain.Entities
{
    public class ScenarioTriggerEntity : IEntity
    {
        public int Id { get; set; }

        public int ScenarioId { get; set; }

        public ScenarioEntity Scenario { get; set; }

        public string Description { get; set; }

        public ScenarioTriggerType Type { get; set; }

        public DateTime? InitialTime { get; set; }

        public DateTime? FinalTime { get; set; }

        public List<ExternalSensorEntity> ExternalSensors { get; set; }

        public List<SensorEntity> Sensors { get; set; }
    }
}