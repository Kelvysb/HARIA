using System;
using System.Collections.Generic;
using HARIA.Domain.Abstractions.DTOs;
using HARIA.Domain.Enums;

namespace HARIA.Domain.DTOs
{
    public class ScenarioTrigger : IDTO
    {
        public int Id { get; set; }

        public int ScenarioId { get; set; }

        public string Description { get; set; }

        public ScenarioTriggerType Type { get; set; }

        public DateTime? InitialTime { get; set; }

        public DateTime? FinalTime { get; set; }

        public List<ExternalSensor> ExternalSensors { get; set; }

        public List<Sensor> Sensors { get; set; }
    }
}