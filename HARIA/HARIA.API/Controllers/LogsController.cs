using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class LogsController : ControllerBase<Log>
    {
        public LogsController(IServiceBase<Log> service) : base(service)
        {
        }
    }
}