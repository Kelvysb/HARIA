using HARIA.Domain.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace HARIA.Diagnostic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var config = new ConfigurationBuilder()
                        .UseDotEnv("../../Docker/.env")
                        .AddEnvironmentVariables()
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddCommandLine(args)
                        .Build();

                    webBuilder
                    .UseConfiguration(config)
                    .UseUrls($"http://*:{config["DIAGNOSTIC_PORT"]}")
                    .UseStartup<Startup>();
                });
    }
}