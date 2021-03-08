using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class NodesRepository : RepositoryBase<NodeEntity>, INodesRepository
    {
        public NodesRepository(IContext context) : base(context)
        {
        }

        public override Task<List<NodeEntity>> GetAll()
        {
            return dbSet
                .Include(t => t.Actuators)
                .Include(t => t.Sensors)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<NodeEntity> GetByCode(string code)
        {
            return dbSet
                .Where(t => t.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase))
                .Include(t => t.Actuators)
                .Include(t => t.Sensors)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public override Task<NodeEntity> GetById(int id)
        {
            return dbSet
                .Where(t => t.Id == id)
                .Include(t => t.Actuators)
                .Include(t => t.Sensors)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}