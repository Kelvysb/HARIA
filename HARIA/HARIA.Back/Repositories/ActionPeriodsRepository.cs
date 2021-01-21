using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.Core.Repositories
{
    public class ActionPeriodsRepository : RepositoryBase<ActionPeriod>, IActionPeriodsRepository
    {
        public ActionPeriodsRepository(Context context) : base(context)
        {
        }
    }
}