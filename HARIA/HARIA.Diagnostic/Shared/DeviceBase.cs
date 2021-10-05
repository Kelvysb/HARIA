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

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        [Parameter]
        public DeviceData DeviceData { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public async Task DeviceStateChange(string elementTitle)
        {
            var message = JsonSerializer.Serialize(DeviceData, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            await MQTTService.SendMessage(message, $"devices/{DeviceData.DeviceId}/set");
        }

        protected override async Task OnInitializedAsync()
        {
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            await base.OnInitializedAsync();
        }
    }
}