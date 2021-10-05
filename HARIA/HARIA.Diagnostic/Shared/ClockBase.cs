using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HARIA.Diagnostic.Shared
{
    public class ClockBase : ComponentBase
    {
        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public bool Running { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            _ = Task.Run(() => TimerRun());
            await base.OnInitializedAsync();
        }

        private async Task TimerRun()
        {
            while (Running)
            {
                await InvokeAsync(StateHasChanged);
                Thread.Sleep(1000);
            }
        }
    }
}