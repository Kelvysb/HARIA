using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ActionEventsRepository : RepositoryBase<ActionEventEntity>, IActionEventsRepository
    {
        public ActionEventsRepository(IContext context) : base(context)
        {
        }
    }
}