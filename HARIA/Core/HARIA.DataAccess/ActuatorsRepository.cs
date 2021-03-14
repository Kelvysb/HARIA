using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class ActuatorsRepository : RepositoryBase<ActuatorEntity>, IActuatorsRepository
    {
        public ActuatorsRepository(IContext context) : base(context)
        {
        }

        public Task<ActuatorEntity> GetByCode(string code)
        {
            return dbSet
                .Where(t => t.Code.Equals(code))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public Task<List<ActuatorEntity>> GetByDevice(int id)
        {
            return dbSet
                .Where(t => t.NodeId == id)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}