using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class Scenario : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public string Icon { get; set; }

        public string Color { get; set; }

        public List<Action> Actions { get; set; }

        public List<ScenarioTrigger> Triggers { get; set; }
    }
}