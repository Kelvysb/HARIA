using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class LogsService : ServiceBase<LogEntity, Log>, ILogsService
    {
        public LogsService(ILogsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}