using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;

namespace HARIA.Domain.Abstractions.Client
{
    public interface IActuatorsClient : IClientBase<Actuator>
    {
        Task<Actuator> GetByCode(string code, string token);

        Task<List<Actuator>> GetByDevice(int deviceId, string token);
    }
}