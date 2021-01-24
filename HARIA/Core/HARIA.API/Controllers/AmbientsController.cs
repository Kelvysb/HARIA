using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class AmbientsController : ControllerBase<AmbientEntity, Ambient>
    {
        public AmbientsController(IAmbientsService service) : base(service)
        {
        }
    }
}