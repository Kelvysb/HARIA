using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.Core.Repositories
{
    public class AmbientsRepository : RepositoryBase<Ambient>, IAmbientsRepository
    {
        public AmbientsRepository(Context context) : base(context)
        {
        }
    }
}