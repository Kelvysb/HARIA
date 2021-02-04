using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class Permission : IDTO
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}