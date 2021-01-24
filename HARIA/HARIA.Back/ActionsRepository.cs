using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ActionsRepository : RepositoryBase<Action>, IActionsRepository
    {
        public ActionsRepository(IContext context) : base(context)
        {
        }
    }
}