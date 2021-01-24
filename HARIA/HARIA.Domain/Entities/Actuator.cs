using System;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class Actuator : IEntity
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public int AmbientId { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public bool LockState { get; set; }

        public int DefaultActiveTime { get; set; }

        public DateTime DeactivationTime { get; set; }

        public DateTime LastStateChange { get; set; }
    }
}