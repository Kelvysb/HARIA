using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class UsersController : ControllerBase<User>
    {
        public UsersController(IUsersService service) : base(service)
        {
        }
    }
}