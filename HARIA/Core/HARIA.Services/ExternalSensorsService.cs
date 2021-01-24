using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ExternalSensorsService : ServiceBase<ExternalSensorEntity, ExternalSensor>, IExternalSensorsService
    {
        public ExternalSensorsService(IExternalSensorsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}