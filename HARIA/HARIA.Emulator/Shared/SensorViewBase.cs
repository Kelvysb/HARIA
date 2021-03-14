using System;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Emulator.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Emulator.Shared
{
    public class SensorViewBase : ComponentBase
    {
        [Parameter]
        public NodeGroup NodeGroup { get; set; }

        [Parameter]
        public Sensor Sensor { get; set; }

        [Inject]
        public IHariaServices hariaServices { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public string ValueHandler
        {
            get
            {
                return Sensor.Value.ToString();
            }
            set
            {
                Sensor.Value = int.Parse(value);
                UpdateStatus();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            hariaServices.StateChange += ((s, e) => StateHasChanged());
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
        }

        public async Task Confirm()
        {
            var messages = await hariaServices.SendNodeMessage(NodeGroup.Node);
            NodeGroup.NotifyNode(this, messages);
        }

        private void UpdateStatus()
        {
            Sensor.Active =
                Sensor.Value >= Sensor.ActiveLowerBound
                && Sensor.Value <= Sensor.ActiveUpperBound;
            Sensor.Message =
                Sensor.Active
                ? $"E;ACTIVE;{DateTime.Now.ToString("HH:mm:ss")}"
                : $"E;INACTIVE;{DateTime.Now.ToString("HH:mm:ss")}";
        }
    }
}