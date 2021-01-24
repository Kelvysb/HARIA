using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class AmbientsRepository : RepositoryBase<AmbientEntity>, IAmbientsRepository
    {
        public AmbientsRepository(IContext context) : base(context)
        {
        }
    }
}