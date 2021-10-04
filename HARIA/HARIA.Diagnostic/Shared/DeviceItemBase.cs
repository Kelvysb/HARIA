using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Enums;
using HARIA.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace HARIA.Diagnostic.Shared
{
    public class DeviceItemBase : ComponentBase
    {
        [Inject]
        public IMqttService MQTTService { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public DigitalElement DeviceItem { get; set; }

        [Parameter]
        public IoElementType DeviceItemType { get; set; }

        [Parameter]
        public EventCallback<string> OnStateChange { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public async Task ChangeState()
        {
            DeviceItem.Value = !DeviceItem.Value;
            await OnStateChange.InvokeAsync(Title);
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            await base.OnInitializedAsync();
        }
    }
}