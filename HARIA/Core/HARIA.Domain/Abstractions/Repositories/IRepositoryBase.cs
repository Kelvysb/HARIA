using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : IEntity
    {
        Task<int> Add(TEntity entity);

        Task<int> Delete(int id);

        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<int> Update(TEntity entity);
    }
}