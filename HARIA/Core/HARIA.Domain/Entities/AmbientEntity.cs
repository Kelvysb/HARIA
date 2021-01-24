using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class AmbientEntity : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<SensorEntity> Sensors { get; set; }

        public List<ActuatorEntity> Actuators { get; set; }
    }
}