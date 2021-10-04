using Microsoft.Extensions.Configuration;

namespace HARIA.Domain.Extensions
{
    public static class ConfigurationExtenssions
    {
        public static IConfigurationBuilder UseDotEnv(this IConfigurationBuilder builder, string envFilePath)
        {
            return builder.Add(new DotEnvConfigProvider(envFilePath));
        }
    }
}