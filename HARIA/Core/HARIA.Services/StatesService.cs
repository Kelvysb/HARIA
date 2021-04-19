using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class StatesService : ServiceBase<StateEntity, State>, IStatesService
    {
        private readonly IStatesRepository statesRepository;

        public StatesService(IStatesRepository repository, IMapper mapper) : base(repository, mapper)
        {
            statesRepository = repository;
        }

        public async Task<State> GetState(string key)
        {
            StateEntity state = await GetStateByKey(key);
            return mapper.Map<StateEntity, State>(state);
        }

        public async Task<Dictionary<string, string>> GetStateDictionary()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            List<StateEntity> states = await repository.GetAll();
            if (states.Any())
            {
                result = new Dictionary<string, string>(states.Select(s => new KeyValuePair<string, string>(s.Key, s.Value)));
            }
            return result;
        }

        public async Task UpdateState(string key, string value)
        {
            StateEntity state = await GetStateByKey(key);
            state.Value = value;
            await statesRepository.Update(state);
        }

        private async Task<StateEntity> GetStateByKey(string key)
        {
            var state = await statesRepository.GetByKey(key);
            if (state == null)
            {
                throw new KeyNotFoundException();
            }
            return state;
        }
    }
}