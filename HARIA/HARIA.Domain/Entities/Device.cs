using System;
using System.Collections.Generic;

namespace HARIA.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public List<Sensor> Sensors { get; set; }

        public List<Actuator> Actuators { get; set; }

        public DateTime LastActivity { get; set; }
    }
}