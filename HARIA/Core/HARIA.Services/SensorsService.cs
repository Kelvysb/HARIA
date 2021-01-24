using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class SensorsService : ServiceBase<SensorEntity, Sensor>, ISensorsService
    {
        public SensorsService(ISensorsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}