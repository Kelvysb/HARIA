using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface IUsersRepository : IRepositoryBase<UserEntity>
    {
        public Task<UserEntity> GetByName(string name);
    }
}