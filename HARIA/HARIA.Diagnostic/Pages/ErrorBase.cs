using System;
using System.Threading.Tasks;
using HARIA.Diagnostic.Abstractions;
using Microsoft.AspNetCore.Components;

namespace HARIA.Diagnostic.Pages
{
    public class ErrorBase : ComponentBase
    {
        [Parameter]
        public Exception exception { get; set; }

        [Inject]
        public IHariaDiagnosticService HariaDiagnosticService { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public bool Collapsed { get; set; } = true;

        public void ToggleCollapseState()
        {
            Collapsed = !Collapsed;
        }

        protected override async Task OnInitializedAsync()
        {
            HariaDiagnosticService.StateChange += ((s, e) => StateHasChanged());
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            HariaDiagnosticService.State.CurrentLocation = Translate.Error;
        }
    }
}