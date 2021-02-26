using System.Threading.Tasks;
using HARIA.Domain.DTOs;

namespace HARIA.Domain.Abstractions.Client
{
    public interface IUsersClient : IClientBase<User>
    {
        Task<User> Login(User login);

        Task<bool> ChangePassword(User login, string token);
    }
}