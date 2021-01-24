using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ExternalSensorsController : ControllerBase<ExternalSensorEntity, ExternalSensor>
    {
        public ExternalSensorsController(IExternalSensorsService service) : base(service)
        {
        }
    }
}