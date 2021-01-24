using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class DevicesRepository : RepositoryBase<DeviceEntity>, IDevicesRepository
    {
        public DevicesRepository(IContext context) : base(context)
        {
        }
    }
}