﻿using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Enums;

namespace HARIA.Domain.Entities
{
    public class Action : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public ActionType Type { get; set; }

        public List<Sensor> Sensors { get; set; }

        public List<ExternalSensor> ExternalSensors { get; set; }

        public bool StaticState { get; set; }

        public List<ActionPeriod> ActionPeriods { get; set; }

        public List<Actuator> Actuators { get; set; }

        public List<ExternalActuator> ExternalActuators { get; set; }

        public string ActuatorMessage { get; set; }

        public bool Invert { get; set; }

        public List<Scenario> Scenarios { get; set; }
    }
}