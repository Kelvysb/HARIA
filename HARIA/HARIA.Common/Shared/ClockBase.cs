using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HARIA.Common.Shared
{
    public class ClockBase : ComponentBase
    {
        [Parameter]
        public string Class { get; set; } = "clock-container";

        [Parameter]
        public string DateTimeFormat { get; set; } = "G";

        public bool Running { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
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