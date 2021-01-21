using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Core.Services
{
    public class ExternalSensorsService : ServiceBase<ExternalSensor>, IExternalSensorsService
    {
        public ExternalSensorsService(IExternalSensorsRepository repository) : base(repository)
        {
        }
    }
}