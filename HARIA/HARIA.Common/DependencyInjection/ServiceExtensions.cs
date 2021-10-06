using HARIA.Common.Abstractions;
using HARIA.Common.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace HARIA.Common.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHariaCommon(this IServiceCollection services)
        {
            services.AddScoped<IHashHelper, HashHelper>();
            services.AddScoped<ILocalStorageHelper, LocalStorageHelper>();

            return services;
        }
    }
}