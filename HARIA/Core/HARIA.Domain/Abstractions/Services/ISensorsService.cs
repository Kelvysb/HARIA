using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Services
{
    public interface ISensorsService : IServiceBase<SensorEntity, Sensor>
    {
        Task<Sensor> GetByCode(string code);
        Task<List<Sensor>> GetByDevice(int id);
    }
}