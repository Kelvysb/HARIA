using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class ScenariosClient : ClientBase<Scenario>, IScenariosClient
    {
        public ScenariosClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "Scenarios")
        {
        }
    }
}