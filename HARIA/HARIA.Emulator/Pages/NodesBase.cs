using System.Linq;
using System.Threading.Tasks;
using HARIA.Emulator.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Emulator.Pages
{
    public class NodesBase : ComponentBase
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
            hariaServices.ReloadData += (async (s, e) => await LoadNodes(true));
            Translate = await I18nText.GetTextTableAsync<I18nText.Text>(this);
            hariaServices.State.CurrentLocation = Translate.Nodes;
            await LoadNodes(false);
        }

        public async Task LoadNodes(bool force)
        {
            if (!force && hariaServices.State.NodeGroups.Any())
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
                    hariaServices.State.NodeGroups = nodes.Select(n => new NodeGroup(n, hariaServices)).ToList();
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
                await LoadNodes(true);
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