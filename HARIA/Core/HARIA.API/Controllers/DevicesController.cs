using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class DevicesController : ControllerBase<DeviceEntity, Device>
    {
        public DevicesController(IDevicesService service) : base(service)
        {
        }
    }
}