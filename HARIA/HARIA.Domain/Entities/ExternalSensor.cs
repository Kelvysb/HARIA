using System;

namespace HARIA.Domain.Entities
{
    public class ExternalSensor
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public bool Active { get; set; }

        public string Condition { get; set; }

        public string Script { get; set; }

        public DateTime LastStateChange { get; set; }
    }
}