using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ExternalActuatorsRepository : RepositoryBase<ExternalActuatorEntity>, IExternalActuatorsRepository
    {
        public ExternalActuatorsRepository(IContext context) : base(context)
        {
        }
    }
}