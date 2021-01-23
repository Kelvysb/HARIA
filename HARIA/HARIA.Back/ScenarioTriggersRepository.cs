using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ScenarioTriggersRepository : RepositoryBase<ScenarioTrigger>, IScenarioTriggersRepository
    {
        public ScenarioTriggersRepository(Context context) : base(context)
        {
        }
    }
}