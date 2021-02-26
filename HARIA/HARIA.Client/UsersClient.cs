using System.Net.Http.Json;
using System.Threading.Tasks;
using HARIA.Client.Extenssions;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class UsersClient : ClientBase<User>, IUsersClient
    {
        public UsersClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "Users")
        {
        }

        public async Task<User> Login(User login)
        {
            var content = JsonContent.Create(login);
            var response = await httpClient
                .PostAsync($"{api}/Login", content);
            return await response.CheckResult<User>();
        }

        public async Task<bool> ChangePassword(User login, string token)
        {
            var content = JsonContent.Create(login);
            var response = await httpClient
                .AddJWT(token)
                .PostAsync($"{api}/ChangePassword", content);
            return response.CheckResult();
        }
    }
}