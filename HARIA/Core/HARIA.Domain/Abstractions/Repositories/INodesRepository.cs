using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface INodesRepository : IRepositoryBase<NodeEntity>
    {
        Task<NodeEntity> GetByCode(string code);
    }
}