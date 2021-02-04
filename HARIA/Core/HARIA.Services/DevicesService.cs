using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class DevicesService : ServiceBase<DeviceEntity, Device>, IDevicesService
    {
        private IDevicesRepository devicesRepository;

        public DevicesService(IDevicesRepository repository, IMapper mapper) : base(repository, mapper)
        {
            devicesRepository = repository;
        }

        public async Task<Device> GetByCode(string code)
        {
            DeviceEntity entity = await devicesRepository.GetByCode(code);
            return mapper.Map<Device>(entity);
        }
    }
}