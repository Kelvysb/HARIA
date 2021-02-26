using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface IExternalSensorsRepository : IRepositoryBase<ExternalSensorEntity>
    {
        Task<ExternalSensorEntity> GetBycode(string code);
    }
}