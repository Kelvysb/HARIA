using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class UsersController : ControllerBase<UserEntity, User>
    {
        public UsersController(IUsersService service) : base(service)
        {
        }
    }
}