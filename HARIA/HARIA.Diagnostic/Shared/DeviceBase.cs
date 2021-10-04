using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace HARIA.Diagnostic.Shared
{
    public class DeviceBase : ComponentBase
    {
        [Inject]
        public IMqttService MQTTService { get; set; }

        [Parameter]
        public DeviceData DeviceData { get; set; }

        public async Task DeviceStateChange(string elementTitle)
        {
            var message = JsonSerializer.Serialize(DeviceData, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            await MQTTService.SendMessage(message, $"devices/{DeviceData.DeviceId}/set");
        }
    }
}