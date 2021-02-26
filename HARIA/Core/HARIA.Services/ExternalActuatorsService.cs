using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ExternalActuatorsService : ServiceBase<ExternalActuatorEntity, ExternalActuator>, IExternalActuatorsService
    {
        private readonly IExternalActuatorsRepository externalActuatorsRepository;

        public ExternalActuatorsService(IExternalActuatorsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            externalActuatorsRepository = repository;
        }

        public async Task<ExternalActuator> GetByCode(string code)
        {
            ExternalActuatorEntity entity = await externalActuatorsRepository.GetBycode(code);
            return mapper.Map<ExternalActuator>(entity);
        }
    }
}