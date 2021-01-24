using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ActionEventPeriodsRepository : RepositoryBase<ActionEventPeriodEntity>, IActionEventPeriodsRepository
    {
        public ActionEventPeriodsRepository(IContext context) : base(context)
        {
        }
    }
}