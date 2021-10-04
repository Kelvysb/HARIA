using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Diagnostic.Abstractions;
using HARIA.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace HARIA.Diagnostic.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IHariaDiagnosticService HariaDiagnosticService { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public List<DeviceData> Devices { get; set; } = new List<DeviceData>();

        public bool Loading { get; set; } = true;

        public async Task DevicecDataUpdateAsync(DeviceData deviceData)
        {
            try
            {
                var device = Devices.FirstOrDefault(d => d.DeviceId == deviceData.DeviceId);
                if (device == null)
                {
                    Devices.Add(deviceData);
                }
                else
                {
                    device.Sensors = deviceData.Sensors;
                    device.Actuators = deviceData.Actuators;
                    device.Datetime = deviceData.Datetime;
                    device.DeviceName = deviceData.DeviceName;
                }
                Loading = false;
                await InvokeAsync(StateHasChanged);
            }
            catch (System.Exception ex)
            {
                HariaDiagnosticService.HandleError(ex);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await HariaDiagnosticService.MqttService.Subscribe(async deviceData => await DevicecDataUpdateAsync(deviceData));
            await HariaDiagnosticService.MqttService.StartPublisher();
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            await base.OnInitializedAsync();
        }
    }
}