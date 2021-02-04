using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface ISensorsRepository : IRepositoryBase<SensorEntity>
    {
        Task<SensorEntity> GetByCode(string code);

        Task<List<SensorEntity>> GetByDevice(int id);
    }
}