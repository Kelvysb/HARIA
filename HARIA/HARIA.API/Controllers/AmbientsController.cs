using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class AmbientsController : ControllerBase<Ambient>
    {
        public AmbientsController(IAmbientsService service) : base(service)
        {
        }
    }
}