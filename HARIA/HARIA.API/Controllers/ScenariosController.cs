using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ScenariosController : ControllerBase<Scenario>
    {
        public ScenariosController(IServiceBase<Scenario> service) : base(service)
        {
        }
    }
}