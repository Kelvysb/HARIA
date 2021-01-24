using System;
using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class DeviceEntity : IEntity
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public List<SensorEntity> Sensors { get; set; }

        public List<ActuatorEntity> Actuators { get; set; }

        public DateTime LastActivity { get; set; }
    }
}