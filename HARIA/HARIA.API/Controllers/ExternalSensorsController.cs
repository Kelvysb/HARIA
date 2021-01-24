using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ExternalSensorsController : ControllerBase<ExternalSensor>
    {
        public ExternalSensorsController(IExternalSensorsService service) : base(service)
        {
        }
    }
}