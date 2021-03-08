using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class NodesController : ControllerBase<NodeEntity, Node>
    {
        public NodesController(INodesService service) : base(service)
        {
        }
    }
}