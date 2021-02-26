using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class SensorsRepository : RepositoryBase<SensorEntity>, ISensorsRepository
    {
        public SensorsRepository(IContext context) : base(context)
        {
        }

        public Task<SensorEntity> GetByCode(string code)
        {
            return dbSet
                .Where(t => t.Code.Equals(code))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public Task<List<SensorEntity>> GetByDevice(int id)
        {
            return dbSet
                .Where(t => t.DeviceId == id)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}