using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class LogsClient : ClientBase<Log>, ILogsClient
    {
        public LogsClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "Logs")
        {
        }
    }
}