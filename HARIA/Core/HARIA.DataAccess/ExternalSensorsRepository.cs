using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class ExternalSensorsRepository : RepositoryBase<ExternalSensorEntity>, IExternalSensorsRepository
    {
        public ExternalSensorsRepository(IContext context) : base(context)
        {
        }

        public Task<ExternalSensorEntity> GetBycode(string code)
        {
            return dbSet
                .Where(t => t.Code.Equals(code))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}