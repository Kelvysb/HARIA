using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class LogsController : ControllerBase<Log>
    {
        public LogsController(ILogsService service) : base(service)
        {
        }
    }
}