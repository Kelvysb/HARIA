using System.Threading.Tasks;
using HARIA.Front.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Front.Components
{
    public class ScenarioListBase : ComponentBase
    {
        [Inject]
        public IHariaServices hariaServices { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        protected override async Task OnInitializedAsync()
        {
            hariaServices.StateChange += ((s, e) => StateHasChanged());
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            hariaServices.State.CurrentLocation = Translate.Dashboard;
        }
    }
}