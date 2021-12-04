using HARIA.Domain.Abstractions.Data;
using HARIA.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HARIA.Data.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHariaData(this IServiceCollection services)
        {
            services.AddSingleton(s =>
            {
                var config = s.GetService<IConfiguration>();
                return new MongoDbConfig
                {
                    ConnectionString = config["MONGODB_CONNECTION_STRING"],
                    DatabaseName = config["MONGODB_DATABASE_NAME"] ?? "HARIA"
                };
            });

            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddSingleton<IDeviceDataRepository, DeviceDataRepository>();

            return services;
        }
    }
}