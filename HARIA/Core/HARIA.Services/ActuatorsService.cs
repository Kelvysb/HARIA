using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ActuatorsService : ServiceBase<ActuatorEntity, Actuator>, IActuatorsService
    {
        private IActuatorsRepository actuatorsRepository;

        public ActuatorsService(IActuatorsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            actuatorsRepository = repository;
        }

        public async Task<Actuator> GetByCode(string code)
        {
            ActuatorEntity entity = await actuatorsRepository.GetByCode(code);
            return mapper.Map<Actuator>(entity);
        }

        public async Task<List<Actuator>> GetByDevice(int id)
        {
            List<ActuatorEntity> entity = await actuatorsRepository.GetByDevice(id);
            return mapper.Map<List<Actuator>>(entity);
        }
    }
}