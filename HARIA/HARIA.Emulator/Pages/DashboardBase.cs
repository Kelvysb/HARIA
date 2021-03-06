using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Emulator.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Emulator.Pages
{
    public class DashboardBase : ComponentBase
    {
        [Inject]
        public IHariaServices hariaServices { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public List<Device> Devices { get; set; } = new List<Device>();

        protected override async Task OnInitializedAsync()
        {
            hariaServices.StateChange += ((s, e) => StateHasChanged());
            hariaServices.StateChange += (async (s, e) => await LoadDevices());
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            hariaServices.State.CurrentLocation = Translate.Dashboard;
            await LoadDevices();
        }

        public async Task LoadDevices()
        {
            try
            {
                if (hariaServices.State.LoggedUser != null)
                {
                    Devices = await hariaServices.GetDevices();
                }
            }
            catch (System.Exception e)
            {
                hariaServices.HandleError(e);
            }
            finally
            {
                StateHasChanged();
            }
        }

        public async Task AddDefaultDevices()
        {
            try
            {
                await hariaServices.AddDefaultDevices(await I18nText.GetTextTableAsync<I18nText.DefaultData>(this));
                await LoadDevices();
            }
            catch (System.Exception e)
            {
                hariaServices.HandleError(e);
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}