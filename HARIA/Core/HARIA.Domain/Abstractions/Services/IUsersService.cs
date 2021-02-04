using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IUsersService : IServiceBase<UserEntity, User>
    {
        public Task<bool> ChangePassword(User login);

        public Task<User> Login(User login);
    }
}