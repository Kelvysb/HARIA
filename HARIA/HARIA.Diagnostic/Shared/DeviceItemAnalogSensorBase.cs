using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace HARIA.Diagnostic.Shared
{
    public class DeviceItemAnalogSensorBase : ComponentBase
    {
        [Inject]
        public IMqttService MQTTService { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        [Parameter]
        public AnalogElement DeviceItem { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        protected override async Task OnInitializedAsync()
        {
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            await base.OnInitializedAsync();
        }
    }
}