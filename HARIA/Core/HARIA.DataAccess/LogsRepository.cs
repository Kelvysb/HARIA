using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class LogsRepository : RepositoryBase<LogEntity>, ILogsRepository
    {
        public LogsRepository(IContext context) : base(context)
        {
        }
    }
}