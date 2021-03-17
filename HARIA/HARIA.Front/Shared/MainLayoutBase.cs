using System.Threading.Tasks;
using HARIA.Common.Helpers;
using HARIA.Domain.DTOs;
using HARIA.Front.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Front.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject]
        public IHariaServices hariaServices { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public IHashHelper hashHelper { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public string Theme { get; set; }

        public string SidebarCssClass => Expanded || hariaServices.State.MenuPinned ? "sidebar-expanded" : "sidebar-collapsed";

        public string ContentCssClass => Expanded || hariaServices.State.MenuPinned ? "content-expanded" : "content-collapsed";

        public bool Expanded { get; set; } = false;

        public User LoginUser { get; set; } = new User();

        public string LoginMessage { get; set; } = "";

        public bool LoginFail { get; set; } = false;

        public bool TopRowHover { get; set; } = false;

        public bool Loading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            hariaServices.StateChange += ((s, e) => StateHasChanged());
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            Theme = "default";
            LoginMessage = Translate.AdminOnly;
            await hariaServices.CheckLoggedUser();
            Loading = false;
            StateHasChanged();
        }

        public void ExpandSidebar()
        {
            if (hariaServices.State.LoggedUser != null)
            {
                Expanded = true;
            }
        }

        public void CollapseSidebar()
        {
            if (hariaServices.State.LoggedUser != null)
            {
                Expanded = false;
            }
        }

        public async Task Login()
        {
            try
            {
                Loading = true;
                StateHasChanged();
                await hariaServices.Login(LoginUser.Name, await hashHelper.GetMD5(LoginUser.PasswordHash));
                LoginFail = false;
                LoginMessage = Translate.AdminOnly;
            }
            catch (System.Exception)
            {
                LoginMessage = Translate.LoginFail;
                LoginFail = true;
            }
            finally
            {
                Loading = false;
                StateHasChanged();
            }
        }

        public async Task LogOut()
        {
            try
            {
                Loading = true;
                StateHasChanged();
                await hariaServices.LogOut();
                NavManager.NavigateTo("/");
            }
            catch (System.Exception e)
            {
                hariaServices.HandleError(e);
            }
            finally
            {
                Loading = false;
                StateHasChanged();
            }
        }
    }
}