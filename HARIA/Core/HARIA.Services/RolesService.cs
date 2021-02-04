using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class RolesService : ServiceBase<RoleEntity, Role>, IRolesService
    {
        public RolesService(IRolesRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}