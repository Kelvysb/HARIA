using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ActuatorsService : ServiceBase<ActuatorEntity, Actuator>, IActuatorsService
    {
        public ActuatorsService(IActuatorsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}