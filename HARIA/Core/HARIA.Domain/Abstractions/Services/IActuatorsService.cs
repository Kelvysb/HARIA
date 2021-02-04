using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IActuatorsService : IServiceBase<ActuatorEntity, Actuator>
    {
        Task<Actuator> GetByCode(string code);

        Task<List<Actuator>> GetByDevice(int id);
    }
}