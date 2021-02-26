using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class AmbientsClient : ClientBase<Ambient>, IAmbientsClient
    {
        public AmbientsClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "Ambients")
        {
        }
    }
}