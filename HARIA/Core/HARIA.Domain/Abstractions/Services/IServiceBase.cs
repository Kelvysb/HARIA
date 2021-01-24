using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IServiceBase<TEntity> where TEntity : class, IEntity, new()
    {
        public Task<int> Add(TEntity entity);

        public Task<int> Delete(int id);

        public Task<List<TEntity>> Get();

        public Task<TEntity> Get(int id);

        public Task<int> Update(TEntity entity);
    }
}