using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class StateEntity : IEntity
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string DefaultValue { get; set; }

        public bool IsSystemDefault { get; set; }
    }
}