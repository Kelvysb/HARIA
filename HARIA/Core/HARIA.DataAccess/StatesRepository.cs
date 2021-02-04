using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class StatesRepository : RepositoryBase<StateEntity>, IStatesRepository
    {
        public StatesRepository(IContext context) : base(context)
        {
        }
    }
}