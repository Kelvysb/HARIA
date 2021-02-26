using Microsoft.AspNetCore.Components;

namespace HARIA.Emulator.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        public string Theme { get; set; }

        public MainLayoutBase()
        {
            Theme = "default";
        }
    }
}