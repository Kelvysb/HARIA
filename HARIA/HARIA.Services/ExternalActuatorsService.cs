using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ExternalActuatorsService : ServiceBase<ExternalActuator>, IExternalActuatorsService
    {
        public ExternalActuatorsService(IExternalActuatorsRepository repository) : base(repository)
        {
        }
    }
}