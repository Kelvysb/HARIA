using System.Threading.Tasks;
using HARIA.Emulator.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Emulator.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        public IHariaServices hariaServices { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public string NavMenuCssClass => CollapseNavMenu ? "collapse" : null;

        public bool CollapseNavMenu { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            hariaServices.StateChange += ((s, e) => StateHasChanged());
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
        }

        public void ToggleNavMenu()
        {
            if (hariaServices.State.LoggedUser != null)
            {
                CollapseNavMenu = !CollapseNavMenu;
            }
        }
    }
}