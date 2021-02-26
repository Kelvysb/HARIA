using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using Microsoft.AspNetCore.Components;

namespace HARIA.Emulator.Shared
{
    public class DeviceViewBase : ComponentBase
    {
        public Device Device { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
    }
}