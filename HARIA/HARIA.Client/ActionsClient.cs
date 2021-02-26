using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class ActionsClient : ClientBase<ActionEvent>, IActionsClient
    {
        public ActionsClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "Actions")
        {
        }
    }
}