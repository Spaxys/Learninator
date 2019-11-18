using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Learninator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(AddCustomConfiguration)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    // clear default logging providers
                    logging.ClearProviders();

                    // add built-in providers manually, as needed 
                    logging.AddConsole();
                    logging.AddDebug();
                });

        public static void AddCustomConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
                builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("Configs/Custom/identitysettings.json", optional: true, reloadOnChange: true);
        }
    }
}
