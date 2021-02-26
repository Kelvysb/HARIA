using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ExternalSensorsService : ServiceBase<ExternalSensorEntity, ExternalSensor>, IExternalSensorsService
    {
        private IExternalSensorsRepository externalSensorsRepository;

        public ExternalSensorsService(IExternalSensorsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            externalSensorsRepository = repository;
        }

        public async Task<ExternalSensor> GetByCode(string code)
        {
            ExternalSensorEntity entity = await externalSensorsRepository.GetBycode(code);
            return mapper.Map<ExternalSensor>(entity);
        }
    }
}