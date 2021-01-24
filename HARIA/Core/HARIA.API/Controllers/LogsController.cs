using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class LogsController : ControllerBase<LogEntity, Log>
    {
        public LogsController(ILogsService service) : base(service)
        {
        }
    }
}