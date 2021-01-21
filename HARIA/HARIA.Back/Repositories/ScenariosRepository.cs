using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.Core.Repositories
{
    public class ScenariosRepository : RepositoryBase<Scenario>, IScenariosRepository
    {
        public ScenariosRepository(Context context) : base(context)
        {
        }
    }
}