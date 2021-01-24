using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ScenarioTriggersController : ControllerBase<ScenarioTriggerEntity, ScenarioTrigger>
    {
        public ScenarioTriggersController(IScenarioTriggersService service) : base(service)
        {
        }
    }
}