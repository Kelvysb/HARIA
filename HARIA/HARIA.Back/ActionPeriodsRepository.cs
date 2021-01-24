using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ActionPeriodsRepository : RepositoryBase<ActionPeriod>, IActionPeriodsRepository
    {
        public ActionPeriodsRepository(IContext context) : base(context)
        {
        }
    }
}