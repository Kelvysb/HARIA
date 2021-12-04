using HARIA.Domain.Models;

namespace HARIA.Integration.Abstractions
{
    public interface IHariaIntegrationService
    {
        Task<List<DeviceState?>> GetActuators();

        Task<DeviceState?> GetActuator(string dedviceId, string itemId);

        Task<List<DeviceState?>> GetSensors();

        Task<DeviceState?> GetSensor(string dedviceId, string itemId);

        Task SetActuator(string dedviceId, string itemId, bool value);
    }
}
