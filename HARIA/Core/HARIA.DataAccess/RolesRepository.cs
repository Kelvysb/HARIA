using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class RolesRepository : RepositoryBase<RoleEntity>, IRolesRepository
    {
        private readonly DbSet<PermissionEntity> permisisonsDbSet;

        public RolesRepository(IContext context) : base(context)
        {
            permisisonsDbSet = context.GetSet<PermissionEntity>();
        }

        public override Task<List<RoleEntity>> GetAll()
        {
            return dbSet
                .Include(t => t.Permissions)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<RoleEntity> GetById(int id)
        {
            return dbSet
                .Include(t => t.Permissions)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public Task<List<PermissionEntity>> GetPermissions()
        {
            return permisisonsDbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<PermissionEntity>> GetPermissions(int roleId)
        {
            return permisisonsDbSet
                .Where(t => t.Id == roleId)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<int> Add(RoleEntity entity)
        {
            entity.Permissions.ForEach(p => context.Atach(p));
            return base.Add(entity);
        }

        public override Task<int> Update(RoleEntity entity)
        {
            entity.Permissions.ForEach(p => context.Atach(p));
            return base.Update(entity);
        }
    }
}