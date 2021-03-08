using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class NodesClient : ClientBase<Node>, INodesClient
    {
        public NodesClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "Nodes")
        {
        }
    }
}