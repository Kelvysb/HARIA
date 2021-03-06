using System;
using System.Threading.Tasks;
using HARIA.Emulator.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Emulator.Pages
{
    public class ErrorBase : ComponentBase
    {
        [Parameter]
        public Exception exception { get; set; }

        [Inject]
        public IHariaServices hariaServices { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public bool Collapsed { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            hariaServices.StateChange += ((s, e) => StateHasChanged());
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            hariaServices.State.CurrentLocation = Translate.Error;
        }

        public void ToggleCollapseState()
        {
            Collapsed = !Collapsed;
        }
    }
}