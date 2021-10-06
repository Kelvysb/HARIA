using Microsoft.AspNetCore.Components;

namespace HARIA.Common.Shared
{
    public class LoadingBase : ComponentBase
    {
        [Parameter]
        public string Image { get; set; } = "/images/Loading_Purple.svg";
    }
}