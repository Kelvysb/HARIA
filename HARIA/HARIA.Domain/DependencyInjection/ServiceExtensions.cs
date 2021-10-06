using HARIA.Domain.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace HARIA.Domain.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHariaDomain(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMapper));

            return services;
        }
    }
}