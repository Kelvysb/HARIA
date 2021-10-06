using HARIA.Domain.Abstractions.Services;

namespace HARIA.Diagnostic.Abstractions
{
    public interface IHariaDiagnosticService : IHariaServiceBase
    {
        public IMqttService MqttService { get; set; }
    }
}