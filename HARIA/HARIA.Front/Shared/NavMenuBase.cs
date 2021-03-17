using System.Threading.Tasks;
using HARIA.Front.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Front.Shared
{
    public class NavMenuBase : ComponentBase
    {
        private const string MENU_PINNED_KEY = "MENU_PINNED";

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
            hariaServices.State.MenuPinned = await hariaServices.LocalStorage.GetItem<bool>(MENU_PINNED_KEY);
        }

        public void ToggleNavMenu()
        {
            if (hariaServices.State.LoggedUser != null)
            {
                CollapseNavMenu = !CollapseNavMenu;
            }
        }

        public async Task ToggleMenuPin()
        {
            hariaServices.State.MenuPinned = !hariaServices.State.MenuPinned;
            await hariaServices.LocalStorage.SetItem(MENU_PINNED_KEY, hariaServices.State.MenuPinned);
        }
    }
}