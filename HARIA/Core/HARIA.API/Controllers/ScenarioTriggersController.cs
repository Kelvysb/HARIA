using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ScenarioTriggersController : ControllerBase<ScenarioTrigger>
    {
        public ScenarioTriggersController(IScenarioTriggersService service) : base(service)
        {
        }
    }
}