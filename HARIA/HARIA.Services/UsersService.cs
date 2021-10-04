using System;
using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Data;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;
using HARIA.Domain.Helpers;
using HARIA.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace HARIA.Services
{
    public class UsersService : ServiceBase<UserEntity, User>, IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IConfiguration configuration;

        public UsersService(
            IUsersRepository usersRepository,
            IMapper mapper,
            IConfiguration configuration)
            : base(usersRepository, mapper)
        {
            this.usersRepository = usersRepository;
            this.configuration = configuration;
        }

        public async Task<bool> ChangePassword(User login, string token)
        {
            var user = await usersRepository.GetById(login.Login);

            if (user.Password.Equals(login.Password, StringComparison.InvariantCultureIgnoreCase))
            {
                user.Password = login.NewPassword;
                await repository.Upsert(user);
                return true;
            }
            else
            {
                throw new UnauthorizedAccessException(login.Login);
            }
        }

        public async Task<User> Login(User login)
        {
            var user = await usersRepository.GetById(login.Login);

            if (user != null && user.Password.Equals(login.Password, StringComparison.InvariantCultureIgnoreCase))
            {
                var result = mapper.Map<User>(user);
                result.Token = AuthHelper.GetToken(result,
                    configuration["AUTH_SECRET"],
                    configuration["AUTH_ISSUER"]);
                return result;
            }
            else
            {
                throw new UnauthorizedAccessException(login.Login);
            }
        }
    }
}