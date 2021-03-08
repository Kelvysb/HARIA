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

        public List<Ambient> Ambients { get; set; } = new List<Ambient>();

        public bool Loading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            hariaServices.StateChange += ((s, e) => StateHasChanged());
            hariaServices.StateChange += (async (s, e) => await LoadAmbients());
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            hariaServices.State.CurrentLocation = Translate.Dashboard;
            await LoadAmbients();
        }

        public async Task LoadAmbients()
        {
            try
            {
                if (hariaServices.State.LoggedUser != null)
                {
                    Loading = true;
                    StateHasChanged();
                    Ambients = await hariaServices.GetAmbients();
                }
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

        public async Task AddDefaultData()
        {
            try
            {
                Loading = true;
                StateHasChanged();
                await hariaServices.AddDefaultData(await I18nText.GetTextTableAsync<I18nText.DefaultData>(this));
                await LoadAmbients();
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