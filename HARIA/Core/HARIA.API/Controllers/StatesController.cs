using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class StatesController : ControllerBase<StateEntity, State>
    {
        public StatesController(IStatesService service) : base(service)
        {
        }
    }
}