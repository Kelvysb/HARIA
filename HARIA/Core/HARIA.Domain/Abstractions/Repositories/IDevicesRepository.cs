using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface IDevicesRepository : IRepositoryBase<DeviceEntity>
    {
        Task<DeviceEntity> GetByCode(string code);
    }
}