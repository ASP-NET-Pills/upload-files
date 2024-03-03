using System.IO;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace UploadFiles
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IConfigurationRoot configuration = BuildConfiguration(args);
            IWebHost webHost = BuildWebHost(args, configuration);

            webHost.Run();
        }

        private static IConfigurationRoot BuildConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        private static IWebHost BuildWebHost(string[] args, IConfigurationRoot configuration)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5001, opt =>
                    {
                        opt.UseHttps();
                    });
                })
                .Build();
        }
    }
}
