using AutoMapper;
using HARIA.Domain.Abstractions.Data;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;
using HARIA.Domain.Models;

namespace HARIA.Services
{
    public class DeviceDataService : ServiceBase<DeviceDataEntity, DeviceData>, IDeviceDataService
    {
        public DeviceDataService(
            IDeviceDataRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}