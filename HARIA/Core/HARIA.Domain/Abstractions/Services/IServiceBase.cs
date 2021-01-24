using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.DTOs;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IServiceBase<TEntity, TDTO>
        where TEntity : class, IEntity, new()
        where TDTO : class, IDTO, new()
    {
        public Task<int> Add(TDTO dto);

        public Task<int> Delete(int id);

        public Task<List<TDTO>> Get();

        public Task<TDTO> Get(int id);

        public Task<int> Update(TDTO dto);
    }
}