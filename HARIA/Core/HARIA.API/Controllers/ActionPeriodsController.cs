using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ActionPeriodsController : ControllerBase<ActionEventPeriodEntity, ActionEventPeriod>
    {
        public ActionPeriodsController(IActionEventPeriodsService service) : base(service)
        {
        }
    }
}