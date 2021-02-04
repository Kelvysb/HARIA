using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface IActuatorsRepository : IRepositoryBase<ActuatorEntity>
    {
        Task<ActuatorEntity> GetByCode(string code);

        Task<List<ActuatorEntity>> GetByDevice(int id);
    }
}