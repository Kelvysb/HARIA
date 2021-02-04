using System;
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

        public Task<DeviceEntity> GetByCode(string code)
        {
            return dbSet
                .Where(t => t.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefaultAsync();
        }
    }
}