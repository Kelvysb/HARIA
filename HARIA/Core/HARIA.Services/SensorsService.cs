using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class SensorsService : ServiceBase<SensorEntity, Sensor>, ISensorsService
    {
        private ISensorsRepository sensorsRepository;

        public SensorsService(ISensorsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            sensorsRepository = repository;
        }

        public async Task<Sensor> GetByCode(string code)
        {
            SensorEntity entity = await sensorsRepository.GetByCode(code);
            return mapper.Map<Sensor>(entity);
        }

        public async Task<List<Sensor>> GetByDevice(int id)
        {
            List<SensorEntity> entity = await sensorsRepository.GetByDevice(id);
            return mapper.Map<List<Sensor>>(entity);
        }
    }
}