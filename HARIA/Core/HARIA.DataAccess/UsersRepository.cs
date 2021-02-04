using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class UsersRepository : RepositoryBase<UserEntity>, IUsersRepository
    {
        public UsersRepository(IContext context) : base(context)
        {
        }

        public override Task<List<UserEntity>> GetAll()
        {
            return dbSet
                .Include(t => t.Roles)
                .ThenInclude(t => t.Permissions)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<UserEntity> GetById(int id)
        {
            return dbSet
                .Where(t => t.Id == id)
                .Include(t => t.Roles)
                .ThenInclude(t => t.Permissions)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public Task<UserEntity> GetByName(string name)
        {
            return dbSet
                .Where(t => t.Name.Equals(name))
                .Include(t => t.Roles)
                .ThenInclude(t => t.Permissions)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public override Task<int> Add(UserEntity entity)
        {
            entity.Roles.ForEach(r => context.Atach(r));
            return base.Add(entity);
        }

        public override Task<int> Update(UserEntity entity)
        {
            entity.Roles.ForEach(r => context.Atach(r));
            return base.Update(entity);
        }
    }
}