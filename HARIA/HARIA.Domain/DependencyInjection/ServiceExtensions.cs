using Elastic.CommonSchema.Serilog;
using HARIA.Domain.Extensions;
using HARIA.Domain.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using Log = Serilog.Log;

namespace HARIA.Domain.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHariaDomain(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMapper));

            return services;
        }

        public static IServiceCollection AddHariaConfiguration(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                       .UseDotEnv("../../Docker/.env")
                       .AddEnvironmentVariables()
                       .AddJsonFile("appsettings.json", optional: false)
                       .Build();
            
            services.AddSingleton<IConfiguration>(config);

            return services;
        }

        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IConfiguration>();
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(config)
               .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(config["ELASTIC_URL"]))
               {
                   ModifyConnectionSettings = x => x.BasicAuthentication(
                        config["ELASTIC_USER"],
                        config["ELASTIC_PASSWORD"]),
                   CustomFormatter = new EcsTextFormatter()
               })
               .WriteTo.Console(new EcsTextFormatter())
               .CreateLogger();

            services.AddSingleton(Log.Logger);

            return services;
        }
    }
}