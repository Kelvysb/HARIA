using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Client.Extenssions;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class ActuatorsClient : ClientBase<Actuator>, IActuatorsClient
    {
        public ActuatorsClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "Actuators")
        {
        }

        public async Task<Actuator> GetByCode(string code, string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .GetAsync($"{api}/ByCode/{code}");
            return await response.CheckResult<Actuator>();
        }

        public async Task<List<Actuator>> GetByDevice(int deviceId, string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .GetAsync($"{api}/ByDevice/{deviceId}");
            return await response.CheckResult<List<Actuator>>();
        }
    }
}