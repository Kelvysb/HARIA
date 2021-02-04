using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class RolesController : ControllerBase<RoleEntity, Role>
    {
        public RolesController(IRolesService service) : base(service)
        {
        }
    }
}