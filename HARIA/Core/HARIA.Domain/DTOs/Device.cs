using System;
using System.Collections.Generic;
using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class Device : IDTO
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public List<Sensor> Sensors { get; set; }

        public List<Actuator> Actuators { get; set; }

        public DateTime LastActivity { get; set; }
    }
}