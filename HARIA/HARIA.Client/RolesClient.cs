using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class RolesClient : ClientBase<Role>, IRolesClient
    {
        public RolesClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "roles")
        {
        }
    }
}