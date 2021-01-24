using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class AmbientsService : ServiceBase<AmbientEntity, Ambient>, IAmbientsService
    {
        public AmbientsService(IAmbientsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}