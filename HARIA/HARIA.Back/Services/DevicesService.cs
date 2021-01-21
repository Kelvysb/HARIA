using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Core.Services
{
    public class DevicesService : ServiceBase<Device>, IDevicesService
    {
        public DevicesService(IDevicesRepository repository) : base(repository)
        {
        }
    }
}