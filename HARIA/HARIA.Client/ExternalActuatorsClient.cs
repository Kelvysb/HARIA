using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class ExternalActuatorsClient : ClientBase<ExternalActuator>, IExternalActuatorsClient
    {
        public ExternalActuatorsClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "ExternalActuators")
        {
        }
    }
}