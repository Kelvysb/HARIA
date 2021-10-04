using System.Collections.Generic;
using System.Threading.Tasks;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IServiceBase<TModel> where TModel : class, new()
    {
        Task Delete(string id);

        Task<TModel> Get(string id);

        Task<List<TModel>> GetAll();

        Task Upsert(TModel dto);
    }
}