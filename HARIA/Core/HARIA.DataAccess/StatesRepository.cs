using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public class StatesRepository : RepositoryBase<StateEntity>, IStatesRepository
    {
        public StatesRepository(IContext context) : base(context)
        {
        }

        public Task<StateEntity> GetByKey(string key)
        {
            return dbSet
                .Where(t => t.Key == key)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}