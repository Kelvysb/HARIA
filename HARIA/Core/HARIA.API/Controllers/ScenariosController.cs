using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ScenariosController : ControllerBase<ScenarioEntity, Scenario>
    {
        public ScenariosController(IScenariosService service) : base(service)
        {
        }
    }
}