using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class StatesService : ServiceBase<StateEntity, State>, IStatesService
    {
        public StatesService(IStatesRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}