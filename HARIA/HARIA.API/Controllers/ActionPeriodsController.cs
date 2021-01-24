using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ActionPeriodsController : ControllerBase<ActionPeriod>
    {
        public ActionPeriodsController(IActionPeriodsService service) : base(service)
        {
        }
    }
}