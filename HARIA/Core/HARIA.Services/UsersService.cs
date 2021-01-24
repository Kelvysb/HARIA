using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class UsersService : ServiceBase<User>, IUsersService
    {
        public UsersService(IUsersRepository repository) : base(repository)
        {
        }
    }
}