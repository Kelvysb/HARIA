using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class UsersService : ServiceBase<UserEntity, User>, IUsersService
    {
        public UsersService(IUsersRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}