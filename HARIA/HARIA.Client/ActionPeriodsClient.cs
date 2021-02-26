using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class ActionPeriodsClient : ClientBase<ActionEventPeriod>, IActionPeriodsClient
    {
        public ActionPeriodsClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "ActionPeriods")
        {
        }
    }
}