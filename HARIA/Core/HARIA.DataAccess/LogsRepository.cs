using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class LogsRepository : RepositoryBase<Log>, ILogsRepository
    {
        public LogsRepository(IContext context) : base(context)
        {
        }
    }
}