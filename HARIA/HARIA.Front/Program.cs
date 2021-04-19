using System;
using System.Net.Http;
using System.Threading.Tasks;
using HARIA.Client;
using HARIA.Common.Helpers;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Front.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace HARIA.Front
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddI18nText();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                return config.GetSection("HARIAApi").Get<HariaApiConfig>();
            });
            services.AddSingleton<IUsersClient, UsersClient>();
            services.AddSingleton<IEngineClient, EngineClient>();
            services.AddSingleton<INodesClient, NodesClient>();
            services.AddSingleton<IAmbientsClient, AmbientsClient>();
            services.AddSingleton<IActionsClient, ActionsClient>();
            services.AddSingleton<IScenariosClient, ScenariosClient>();
            services.AddSingleton<IStatesClient, StatesClient>();
            services.AddSingleton<IHashHelper, HashHelper>();
            services.AddSingleton<ILocalStorageHelper, LocalStorageHelper>();
            services.AddSingleton<IHariaServices, HariaServices>();
        }
    }
}