using System.Threading.Tasks;
using HARIA.Diagnostic.Abstractions;
using HARIA.Domain.Abstractions;
using HARIA.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace HARIA.Diagnostic.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject]
        public IHariaDiagnosticService HariaDiagnosticService { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public IHashHelper hashHelper { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public User LoginUser { get; set; } = new User();

        public string LoginMessage { get; set; } = "";

        public bool LoginFail { get; set; } = false;

        public bool Loading { get; set; } = true;

        public bool TopRowHover { get; set; } = false;

        public async Task Login()
        {
            try
            {
                Loading = true;
                StateHasChanged();
                await HariaDiagnosticService.Login(LoginUser.Login, await hashHelper.GetMD5(LoginUser.Password));
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
                await HariaDiagnosticService.LogOut();
                NavManager.NavigateTo("/");
            }
            catch (System.Exception e)
            {
                HariaDiagnosticService.HandleError(e);
            }
            finally
            {
                Loading = false;
                StateHasChanged();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await HariaDiagnosticService.CheckLoggedUser();
            Loading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            HariaDiagnosticService.StateChange += ((s, e) => StateHasChanged());
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            LoginMessage = Translate.AdminOnly;
        }
    }
}