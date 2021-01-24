using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class SensorsController : ControllerBase<Sensor>
    {
        public SensorsController(ISensorsService service) : base(service)
        {
        }
    }
}