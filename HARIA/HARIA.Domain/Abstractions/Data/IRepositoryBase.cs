using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Data
{
    public interface IRepositoryBase<TEntity>
        where TEntity : EntityBase, new()
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(string id);

        Task BulkUpsert(List<TEntity> entities);

        Task Upsert(TEntity entity);

        Task Delete(string id);
    }
}