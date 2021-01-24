using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class DevicesRepository : RepositoryBase<Device>, IDevicesRepository
    {
        public DevicesRepository(IContext context) : base(context)
        {
        }
    }
}