using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace HARIA.Emulator.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private bool collapseNavMenu = true;

        protected override async Task OnInitializedAsync()
        {
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
        }

        public void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}