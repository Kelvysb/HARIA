using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class ScenarioTriggersClient : ClientBase<ScenarioTrigger>, IScenarioTriggersClient
    {
        public ScenarioTriggersClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "ScenarioTriggers")
        {
        }
    }
}