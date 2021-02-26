using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class DevicesRepository : RepositoryBase<DeviceEntity>, IDevicesRepository
    {
        public DevicesRepository(IContext context) : base(context)
        {
        }

        public override Task<List<DeviceEntity>> GetAll()
        {
            return dbSet
                .Include(t => t.Actuators)
                .Include(t => t.Sensors)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<DeviceEntity> GetByCode(string code)
        {
            return dbSet
                .Where(t => t.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase))
                .Include(t => t.Actuators)
                .Include(t => t.Sensors)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public override Task<DeviceEntity> GetById(int id)
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