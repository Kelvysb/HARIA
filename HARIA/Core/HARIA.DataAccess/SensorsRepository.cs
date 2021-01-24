using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class SensorsRepository : RepositoryBase<Sensor>, ISensorsRepository
    {
        public SensorsRepository(IContext context) : base(context)
        {
        }
    }
}