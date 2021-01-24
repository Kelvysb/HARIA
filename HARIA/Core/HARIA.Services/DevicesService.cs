using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class DevicesService : ServiceBase<DeviceEntity, Device>, IDevicesService
    {
        public DevicesService(IDevicesRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}