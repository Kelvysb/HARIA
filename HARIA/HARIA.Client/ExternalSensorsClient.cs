using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class ExternalSensorsClient : ClientBase<ExternalSensor>, IExternalSensorsClient
    {
        public ExternalSensorsClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "ExternalSensors")
        {
        }
    }
}