using System.Collections.Generic;
using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class Scenario : IDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public string Icon { get; set; }

        public string Color { get; set; }

        public List<ActionEvent> Actions { get; set; }

        public List<ScenarioTrigger> Triggers { get; set; }
    }
}