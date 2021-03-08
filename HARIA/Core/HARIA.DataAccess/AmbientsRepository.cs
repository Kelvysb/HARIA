using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class AmbientsRepository : RepositoryBase<AmbientEntity>, IAmbientsRepository
    {
        public AmbientsRepository(IContext context) : base(context)
        {
        }

        public override Task<List<AmbientEntity>> GetAll()
        {
            return dbSet
                .Include(a => a.Sensors)
                .Include(a => a.Actuators)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<AmbientEntity> GetById(int id)
        {
            return dbSet
                .Where(a => a.Id == id)
                .Include(a => a.Sensors)
                .Include(a => a.Actuators)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}