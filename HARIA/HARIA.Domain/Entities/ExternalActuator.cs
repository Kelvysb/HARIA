using System;
using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class ExternalActuator : IEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Script { get; set; }

        public DateTime LastExecution { get; set; }

        public List<Action> Actions { get; set; }
    }
}