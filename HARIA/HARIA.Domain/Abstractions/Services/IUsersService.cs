using System.Threading.Tasks;
using HARIA.Domain.Models;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IUsersService : IServiceBase<User>
    {
        Task<User> Login(User login);

        Task<bool> ChangePassword(User login, string token);
    }
}