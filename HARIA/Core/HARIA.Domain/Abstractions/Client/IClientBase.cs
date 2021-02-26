using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.Abstractions.Client
{
    public interface IClientBase<TDTO>
        where TDTO : class, IDTO, new()
    {
        Task<bool> Add(TDTO dto, string token);

        Task<bool> Delete(int id, string token);

        Task<List<TDTO>> Get(string token);

        Task<TDTO> Get(int id, string token);

        Task<bool> Update(TDTO dto, string token);
    }
}