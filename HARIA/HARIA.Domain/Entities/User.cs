using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }
    }
}