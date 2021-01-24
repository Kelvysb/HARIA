using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class DevicesController : ControllerBase<Device>
    {
        public DevicesController(IDevicesService service) : base(service)
        {
        }
    }
}