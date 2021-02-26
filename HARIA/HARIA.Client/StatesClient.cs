using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class StatesClient : ClientBase<State>, IStatesClient
    {
        public StatesClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "States")
        {
        }
    }
}