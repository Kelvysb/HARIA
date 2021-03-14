using System;
using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class ExternalSensor : IDTO, IGenericSensor
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public bool Active { get; set; }

        public string Condition { get; set; }

        public string Script { get; set; }

        public string Message { get; set; }

        public DateTime LastStateChange { get; set; }
    }
}