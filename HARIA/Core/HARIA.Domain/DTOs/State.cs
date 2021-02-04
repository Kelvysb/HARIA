using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class State : IDTO
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public string DefaultValue { get; set; }

        public bool IsSystemDefault { get; set; }
    }
}