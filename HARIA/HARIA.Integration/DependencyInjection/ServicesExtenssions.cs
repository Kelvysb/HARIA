using HARIA.Integration.Abstractions;
using HARIA.Integration.Services;
using Microsoft.OpenApi.Models;

namespace HARIA.Integration.DependencyInjection
{
    public static class ServicesExtenssions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IHariaIntegrationService, HariaIntegrationService>();
            return services; 
        }

        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HARIA Integration", Description = "HARIA Integration server", Version = "v1" });
            });
            return builder;
        }

    }
}
