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
            entity.Sensors.ForEach(s => context.Atach(s));
            entity.ExternalSensors.ForEach(s => context.Atach(s));
            entity.Actuators.ForEach(s => context.Atach(s));
            entity.ExternalActuators.ForEach(s => context.Atach(s));
            return base.Add(entity);
        }

        public override Task<int> Update(ActionEventEntity entity)
        {
            entity.Scenarios.ForEach(s => context.Atach(s));
            entity.Sensors.ForEach(s => context.Atach(s));
            entity.ExternalSensors.ForEach(s => context.Atach(s));
            entity.Actuators.ForEach(s => context.Atach(s));
            entity.ExternalActuators.ForEach(s => context.Atach(s));
            return base.Update(entity);
        }
    }
}