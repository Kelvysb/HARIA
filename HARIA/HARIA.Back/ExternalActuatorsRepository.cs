using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ExternalActuatorsRepository : RepositoryBase<ExternalActuator>, IExternalActuatorsRepository
    {
        public ExternalActuatorsRepository(IContext context) : base(context)
        {
        }
    }
}