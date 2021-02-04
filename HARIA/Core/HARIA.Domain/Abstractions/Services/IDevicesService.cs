using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IDevicesService : IServiceBase<DeviceEntity, Device>
    {
        Task<Device> GetByCode(string code);
    }
}