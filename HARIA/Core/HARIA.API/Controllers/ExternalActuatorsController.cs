using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ExternalActuatorsController : ControllerBase<ExternalActuatorEntity, ExternalActuator>
    {
        public ExternalActuatorsController(IExternalActuatorsService service) : base(service)
        {
        }
    }
}