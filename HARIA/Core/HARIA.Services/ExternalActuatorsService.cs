using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ExternalActuatorsService : ServiceBase<ExternalActuatorEntity, ExternalActuator>, IExternalActuatorsService
    {
        public ExternalActuatorsService(IExternalActuatorsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}