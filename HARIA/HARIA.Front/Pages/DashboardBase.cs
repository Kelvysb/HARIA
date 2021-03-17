using System.Linq;
using System.Threading.Tasks;
using HARIA.Front.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Front.Pages
{
    public class DashboardBase : ComponentBase
    {
        [Inject]
        public IHariaServices hariaServices { get; set; }

        [Inject]
        public Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        public I18nText.Text Translate { get; set; } = new I18nText.Text();

        public bool Loading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            hariaServices.StateChange += ((s, e) => StateHasChanged());
            hariaServices.StateChange += (async (s, e) => await HandleStateChangedReload(e.State));
            hariaServices.ReloadData += (async (s, e) => await LoadDashboard(true));
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            hariaServices.State.CurrentLocation = Translate.Dashboard;
            await LoadDashboard(false);
        }

        public async Task LoadDashboard(bool force)
        {
            if (!force && hariaServices.State.Ambients.Any())
            {
                Loading = false;
                StateHasChanged();
                return;
            }

            try
            {
                if (hariaServices.State.LoggedUser != null)
                {
                    Loading = true;
                    StateHasChanged();
                    var nodes = await hariaServices.GetNodes();
                    hariaServices.State.Ambients = await hariaServices.GetAmbients();
                    hariaServices.State.Ambients.ForEach(a =>
                    {
                        a.Sensors = nodes.SelectMany(n => n.Sensors).Where(d => d.AmbientId == a.Id).ToList();
                        a.Actuators = nodes.SelectMany(n => n.Actuators).Where(d => d.AmbientId == a.Id).ToList();
                    });
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

        public void NavigateToManagement()
        {
        }

        private async Task HandleStateChangedReload(string state)
        {
            if (state.Equals("LoggedUser"))
            {
                await LoadDashboard(true);
            }
        }
    }
}