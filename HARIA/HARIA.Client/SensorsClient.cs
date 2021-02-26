using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Client.Extenssions;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class SensorsClient : ClientBase<Sensor>, ISensorsClient
    {
        public SensorsClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "Sensors")
        {
        }

        public async Task<Sensor> GetByCode(string code, string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .GetAsync($"{api}/ByCode/{code}");
            return await response.CheckResult<Sensor>();
        }

        public async Task<List<Sensor>> GetByDevice(int deviceId, string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .GetAsync($"{api}/ByDevice/{deviceId}");
            return await response.CheckResult<List<Sensor>>();
        }
    }
}