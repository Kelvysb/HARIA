using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ExternalSensorsRepository : RepositoryBase<ExternalSensor>, IExternalSensorsRepository
    {
        public ExternalSensorsRepository(IContext context) : base(context)
        {
        }
    }
}