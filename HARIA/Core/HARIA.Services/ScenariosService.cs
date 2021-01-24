using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ScenariosService : ServiceBase<ScenarioEntity, Scenario>, IScenariosService
    {
        public ScenariosService(IScenariosRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}