using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HARIA.Services.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHariaServices(this IServiceCollection services)
        {
            services.AddSingleton(s =>
            {
                var config = s.GetService<IConfiguration>();
                return new MQTTConfig
                {
                    DeviceId = config["MQTT_DEVICE_ID"],
                    Server = config["MQTT_SERVER"],
                    Port = int.Parse(config["MQTT_PORT"]),
                    User = config["MQTT_USER"],
                    Password = config["MQTT_PASSWORD"],
                    TopicsPattern = config["MQTT_TOPICS_PATTERN"]
                };
            });

            services.AddSingleton<IMqttService, MqttService>();
            services.AddSingleton<IUsersService, UsersService>();
            services.AddSingleton<IDeviceDataService, DeviceDataService>();
            services.AddSingleton<IMigrationService, MigrationService>();

            return services;
        }
    }
}