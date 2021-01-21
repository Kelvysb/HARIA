using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.Core.Repositories
{
    public class DevicesRepository : RepositoryBase<Device>, IDevicesRepository
    {
        public DevicesRepository(Context context) : base(context)
        {
        }
    }
}