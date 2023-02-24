using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MobaSpace.Core.Log;

namespace MobaSpace.Web.UI
{
    public class Program
    {
        public static Assembly ASSEMBLY { get; } = typeof(Startup).GetTypeInfo().Assembly;
        public static string ASSEMBLY_VERSION { get; } = ASSEMBLY.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            var host = hostBuilder.Build();
            LogUtils.LoggerFactory = host.Services.GetService<ILoggerFactory>();
            var logger = LogUtils.CreateLogger<Program>();
            logger.LogInformation($"Starting {ASSEMBLY.FullName}");
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
