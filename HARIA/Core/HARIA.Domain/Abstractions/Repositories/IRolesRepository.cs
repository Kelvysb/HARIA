using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface IRolesRepository : IRepositoryBase<RoleEntity>
    {
        public Task<List<PermissionEntity>> GetPermissions();

        public Task<List<PermissionEntity>> GetPermissions(int roleId);
    }
}