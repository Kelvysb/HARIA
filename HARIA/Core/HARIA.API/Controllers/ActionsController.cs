using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ActionsController : ControllerBase<ActionEventEntity, ActionEvent>
    {
        public ActionsController(IActionEventsService service) : base(service)
        {
        }
    }
}