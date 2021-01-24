using System.Collections.Generic;
using HARIA.Domain.Abstractions.DTOs;
using HARIA.Domain.Enums;

namespace HARIA.Domain.DTOs
{
    public class ActionEvent : IDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ActionType Type { get; set; }

        public List<Sensor> Sensors { get; set; }

        public List<ExternalSensor> ExternalSensors { get; set; }

        public bool StaticState { get; set; }

        public List<ActionEventPeriod> ActionPeriods { get; set; }

        public List<Actuator> Actuators { get; set; }

        public List<ExternalActuator> ExternalActuators { get; set; }

        public string ActuatorMessage { get; set; }

        public bool Invert { get; set; }

        public List<Scenario> Scenarios { get; set; }
    }
}