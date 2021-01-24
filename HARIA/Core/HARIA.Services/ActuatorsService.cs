using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ActuatorsService : ServiceBase<Actuator>, IActuatorsService
    {
        public ActuatorsService(IActuatorsRepository repository) : base(repository)
        {
        }
    }
}