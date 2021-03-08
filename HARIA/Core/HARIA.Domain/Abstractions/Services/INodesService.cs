using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Services
{
    public interface INodesService : IServiceBase<NodeEntity, Node>
    {
        Task<Node> GetByCode(string code);
    }
}