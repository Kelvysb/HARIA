using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class SensorsController : ControllerBase<SensorEntity, Sensor>
    {
        public SensorsController(ISensorsService service) : base(service)
        {
        }
    }
}