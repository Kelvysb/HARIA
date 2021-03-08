using System;
using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class Actuator : IDTO
    {
        public int Id { get; set; }

        public int NodeId { get; set; }

        public int AmbientId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public bool LockState { get; set; }

        public int DefaultActiveTime { get; set; }

        public string Message { get; set; }

        public DateTime? DeactivationTime { get; set; }

        public DateTime LastStateChange { get; set; }
    }
}