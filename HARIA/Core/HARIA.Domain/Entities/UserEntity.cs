using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class UserEntity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public List<RoleEntity> Roles { get; set; }
    }
}