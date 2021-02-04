using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class RoleEntity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<PermissionEntity> Permissions { get; set; }

        public List<UserEntity> Users { get; set; }
    }
}