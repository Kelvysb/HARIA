using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Core.Services
{
    public class LogsService : ServiceBase<Log>, ILogsService
    {
        public LogsService(ILogsRepository repository) : base(repository)
        {
        }
    }
}