using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ActionPeriodsService : ServiceBase<ActionPeriod>, IActionPeriodsService
    {
        public ActionPeriodsService(IActionPeriodsRepository repository) : base(repository)
        {
        }
    }
}