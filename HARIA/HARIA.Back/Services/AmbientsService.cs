using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Core.Services
{
    public class AmbientsService : ServiceBase<Ambient>, IAmbientsService
    {
        public AmbientsService(IAmbientsRepository repository) : base(repository)
        {
        }
    }
}