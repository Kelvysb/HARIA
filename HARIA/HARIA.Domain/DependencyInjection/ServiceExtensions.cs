using HARIA.Domain.Abstractions;
using HARIA.Domain.Helpers;
using HARIA.Domain.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace HARIA.Domain.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHariaDomain(this IServiceCollection services)
        {
            services.AddScoped<IHashHelper, HashHelper>();
            services.AddScoped<ILocalStorageHelper, LocalStorageHelper>();

            services.AddAutoMapper(typeof(UserMapper));

            return services;
        }
    }
}