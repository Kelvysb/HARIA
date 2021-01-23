using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ActionsService : ServiceBase<Action>, IActionsService
    {
        public ActionsService(IActionsRepository repository) : base(repository)
        {
        }
    }
}