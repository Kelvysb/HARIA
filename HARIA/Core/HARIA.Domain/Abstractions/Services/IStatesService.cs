using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IStatesService : IServiceBase<StateEntity, State>
    {
        public Task<Dictionary<string, string>> GetStateDictionary();

        public Task UpdateState(string key, string value);

        public Task<State> GetState(string key);
    }
}