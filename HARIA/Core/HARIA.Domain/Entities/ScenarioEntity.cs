using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class ScenarioEntity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public string Icon { get; set; }

        public string Color { get; set; }

        public bool IsDefault { get; set; }

        public List<ActionEventEntity> Actions { get; set; }

        public List<ScenarioTriggerEntity> Triggers { get; set; }
    }
}