using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Enums;

namespace HARIA.Domain.Entities
{
    public class ActionEventEntity : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ActionType Type { get; set; }

        public List<SensorEntity> Sensors { get; set; }

        public List<ExternalSensorEntity> ExternalSensors { get; set; }

        public bool StaticState { get; set; }

        public List<ActionEventPeriodEntity> ActionPeriods { get; set; }

        public List<ActuatorEntity> Actuators { get; set; }

        public List<ExternalActuatorEntity> ExternalActuators { get; set; }

        public string ActuatorMessage { get; set; }

        public bool Invert { get; set; }

        public List<ScenarioEntity> Scenarios { get; set; }
    }
}