using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Core.Services
{
    public class SensorsService : ServiceBase<Sensor>, ISensorsService
    {
        public SensorsService(ISensorsRepository repository) : base(repository)
        {
        }
    }
}