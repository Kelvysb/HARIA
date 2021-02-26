using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class ScenariosRepository : RepositoryBase<ScenarioEntity>, IScenariosRepository
    {
        public ScenariosRepository(IContext context) : base(context)
        {
        }

        public override Task<List<ScenarioEntity>> GetAll()
        {
            return dbSet
                .Include(t => t.Triggers)
                .ThenInclude(t => t.Sensors)
                .Include(t => t.Triggers)
                .ThenInclude(t => t.ExternalSensors)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<ScenarioEntity> GetById(int id)
        {
            return dbSet
                .Where(t => t.Id == id)
                .Include(t => t.Triggers)
                .ThenInclude(t => t.Sensors)
                .Include(t => t.Triggers)
                .ThenInclude(t => t.ExternalSensors)
                .Include(t => t.Actions)
                .ThenInclude(t => t.Actuators)
                .Include(t => t.Actions)
                .ThenInclude(t => t.ExternalActuators)
                .Include(t => t.Actions)
                .ThenInclude(t => t.Sensors)
                .Include(t => t.Actions)
                .ThenInclude(t => t.ExternalSensors)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}