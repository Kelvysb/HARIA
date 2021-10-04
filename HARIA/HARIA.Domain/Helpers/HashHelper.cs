using System.Threading.Tasks;
using HARIA.Domain.Abstractions;
using Microsoft.JSInterop;

namespace HARIA.Domain.Helpers
{
    public class HashHelper : IHashHelper
    {
        private IJSRuntime jSRuntime;

        public HashHelper(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }

        public async Task<string> GetMD5(string input)
        {
            return await jSRuntime.InvokeAsync<string>("md5", input);
        }
    }
}