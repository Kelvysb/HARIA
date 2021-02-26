using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;

namespace HARIA.Domain.Abstractions.Client
{
    public interface ISensorsClient : IClientBase<Sensor>
    {
        Task<Sensor> GetByCode(string code, string token);

        Task<List<Sensor>> GetByDevice(int deviceId, string token);
    }
}