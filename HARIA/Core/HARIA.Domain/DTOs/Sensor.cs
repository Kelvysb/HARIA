using System;
using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class Sensor : IDTO
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public int AmbientId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public int ActiveLowerBound { get; set; }

        public int ActiveUpperBound { get; set; }

        public bool Active { get; set; }

        public string Message { get; set; }

        public DateTime LastStateChange { get; set; }
    }
}