using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ScenariosRepository : RepositoryBase<ScenarioEntity>, IScenariosRepository
    {
        public ScenariosRepository(IContext context) : base(context)
        {
        }
    }
}