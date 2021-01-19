using System.Collections.Generic;

namespace HARIA.Domain.Entities
{
    public class Scenario
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public string Color { get; set; }

        public List<Action> Actions { get; set; }
    }
}