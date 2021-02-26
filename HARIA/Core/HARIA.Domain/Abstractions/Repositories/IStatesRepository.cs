using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface IStatesRepository : IRepositoryBase<StateEntity>
    {
        Task<StateEntity> GetByKey(string key);
    }
}