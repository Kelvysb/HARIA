using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class UsersController : ControllerBase<User>
    {
        public UsersController(IServiceBase<User> service) : base(service)
        {
        }
    }
}