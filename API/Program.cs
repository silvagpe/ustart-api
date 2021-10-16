using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace UStart.API
{
    public class Program
    {
        public static int Main(string[] args)
        {
            // Create the Serilog logger, and configure the sinks
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine("Log", "service.log"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3)
                .WriteTo.Console()                
                .CreateLogger();



            // Wrap creating and running the host in a try-catch block
            try
            {
                var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json")
                .Build();

                ConfigureSerilog(args, configuration);
                Log.Information("Starting host");

                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureSerilog(string[] args, IConfiguration configuration)
        {
            // Serilog.Log.Logger = new LoggerConfiguration()
            //     .Enrich.FromLogContext()
            //     .WriteTo.File(Path.Combine("Log", "service.log"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3)
            //     .ReadFrom.Configuration(configuration)
            //     .CreateLogger();


            // CreateHostBuilder(args).Build().Run();

            // Serilog.Log.CloseAndFlush();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
