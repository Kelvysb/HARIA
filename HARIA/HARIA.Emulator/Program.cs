using System;
using System.Net.Http;
using System.Threading.Tasks;
using HARIA.Client;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace HARIA.Emulator
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
            services.AddSingleton<IDevicesClient, DevicesClient>();
            services.AddSingleton<IActuatorsClient, ActuatorsClient>();
            services.AddSingleton<ISensorsClient, SensorsClient>();
        }
    }
}