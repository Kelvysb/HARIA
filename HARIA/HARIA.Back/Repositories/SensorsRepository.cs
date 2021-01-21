using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.Core.Repositories
{
    public class SensorsRepository : RepositoryBase<Sensor>, ISensorsRepository
    {
        public SensorsRepository(Context context) : base(context)
        {
        }
    }
}