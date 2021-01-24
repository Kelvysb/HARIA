using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ExternalActuatorsController : ControllerBase<ExternalActuator>
    {
        public ExternalActuatorsController(IExternalActuatorsService service) : base(service)
        {
        }
    }
}