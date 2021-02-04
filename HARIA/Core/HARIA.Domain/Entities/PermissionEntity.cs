using System.Collections.Generic;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class PermissionEntity : IEntity
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public List<RoleEntity> Roles { get; set; }
    }
}