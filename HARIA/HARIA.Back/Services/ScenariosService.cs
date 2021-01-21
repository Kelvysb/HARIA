using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Core.Services
{
    public class ScenariosService : ServiceBase<Scenario>, IScenariosService
    {
        public ScenariosService(IScenariosRepository repository) : base(repository)
        {
        }
    }
}