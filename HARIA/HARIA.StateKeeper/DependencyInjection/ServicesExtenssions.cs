using Elastic.CommonSchema.Serilog;
using HARIA.Domain.Extensions;
using HARIA.StateKeeper.Abstractions;
using HARIA.StateKeeper.Services;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace HARIA.StateKeeper.DependencyInjection
{
    public static class ServicesExtenssions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IHariaStateKeeperService, HariaStateKeeperService>();
            return services;
        }
    }
}
