using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.Core.Repositories
{
    public class ExternalSensorsRepository : RepositoryBase<ExternalSensor>, IExternalSensorsRepository
    {
        public ExternalSensorsRepository(Context context) : base(context)
        {
        }
    }
}