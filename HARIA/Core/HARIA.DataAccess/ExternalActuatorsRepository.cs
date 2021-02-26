using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class ExternalActuatorsRepository : RepositoryBase<ExternalActuatorEntity>, IExternalActuatorsRepository
    {
        public ExternalActuatorsRepository(IContext context) : base(context)
        {
        }

        public Task<ExternalActuatorEntity> GetBycode(string code)
        {
            return dbSet
                .Where(t => t.Code.Equals(code))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}