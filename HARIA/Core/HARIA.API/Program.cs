using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace HARIA.API
{
    public class Program
    {
        private const int DefaultPort = 7052;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var port = DefaultPort;

            if (args.Any())
            {
                int.TryParse(args[0], out port);
            }

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .UseUrls($"http://*:{port}");
                });
        }
    }
}