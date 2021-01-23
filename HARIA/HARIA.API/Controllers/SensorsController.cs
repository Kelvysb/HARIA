using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class SensorsController : ControllerBase<Sensor>
    {
        public SensorsController(IServiceBase<Sensor> service) : base(service)
        {
        }
    }
}