using System;
using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;
using HARIA.Domain.Helpers;
using Microsoft.Extensions.Configuration;

namespace HARIA.Services
{
    public class UsersService : ServiceBase<UserEntity, User>, IUsersService
    {
        private IUsersRepository usersRepository;
        private IConfiguration config;

        public UsersService(IUsersRepository repository, IMapper mapper, IConfiguration config) : base(repository, mapper)
        {
            usersRepository = repository;
            this.config = config;
        }

        public override async Task<int> Update(User dto)
        {
            UserEntity current = await repository.GetById(dto.Id);
            UserEntity entity = mapper.Map<UserEntity>(dto);
            entity.PasswordHash = current?.PasswordHash;
            return await repository.Update(entity);
        }

        public async Task<bool> ChangePassword(User login)
        {
            UserEntity user = await repository.GetById(login.Id);

            if (user.PasswordHash.Equals(login.PasswordHash, StringComparison.InvariantCultureIgnoreCase))
            {
                user.PasswordHash = login.NewPasswordHash;
                await repository.Update(user);
                return true;
            }
            else
            {
                throw new UnauthorizedAccessException(login.Name);
            }
        }

        public async Task<User> Login(User login)
        {
            UserEntity user = await usersRepository.GetByName(login.Name);
            if (user != null && user.PasswordHash.Equals(login.PasswordHash, StringComparison.InvariantCultureIgnoreCase))
            {
                User result = mapper.Map<User>(user);
                result.Token = AuthHelper.GetToken(result,
                    config.GetSection("Auth")["Secret"],
                    config.GetSection("Auth")["Issuer"]);
                return result;
            }
            else
            {
                throw new UnauthorizedAccessException(login.Name);
            }
        }
    }
}