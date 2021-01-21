using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.Core.Repositories
{
    public class ActionsRepository : RepositoryBase<Action>, IActionsRepository
    {
        public ActionsRepository(Context context) : base(context)
        {
        }
    }
}