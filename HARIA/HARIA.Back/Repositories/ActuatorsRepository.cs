using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.Core.Repositories
{
    public class ActuatorsRepository : RepositoryBase<Actuator>, IActuatorsRepository
    {
        public ActuatorsRepository(Context context) : base(context)
        {
        }
    }
}