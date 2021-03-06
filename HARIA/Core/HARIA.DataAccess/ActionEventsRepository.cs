using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ActionEventsRepository : RepositoryBase<ActionEventEntity>, IActionEventsRepository
    {
        public ActionEventsRepository(IContext context) : base(context)
        {
        }

        public override Task<int> Add(ActionEventEntity entity)
        {
            entity.Scenarios.ForEach(s => context.Atach(s));
            return base.Add(entity);
        }

        public override Task<int> Update(ActionEventEntity entity)
        {
            entity.Scenarios.ForEach(s => context.Atach(s));
            return base.Update(entity);
        }
    }
}