using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.Core.Repositories
{
    public class LogsRepository : RepositoryBase<Log>, ILogsRepository
    {
        public LogsRepository(Context context) : base(context)
        {
        }
    }
}