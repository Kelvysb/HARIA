using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ActuatorsController : ControllerBase<ActuatorEntity, Actuator>
    {
        public ActuatorsController(IActuatorsService service) : base(service)
        {
        }
    }
}