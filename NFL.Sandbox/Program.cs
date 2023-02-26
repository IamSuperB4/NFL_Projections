using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NFL.Business;
using NFL.Common;
using NFL.Database;
using Serilog;
using System;

namespace NFL.Sandbox
{
    public class Program
    {
        private static IConfigurationRoot configuration;
        private static ILogger logger;

        static void Main(string[] args)
        {
            // be able to read from appsettings.json
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // be able to create logs with Serilog
            logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            // Spins up a service to be able to use services, particularly ChassisService in .Business
            IHost host = CreateHostBuilder(args).Build();
            using IServiceScope scope = host.Services.CreateScope();

            IGamesService service = scope.ServiceProvider.GetRequiredService<IGamesService>();
            IAdminService adminService = scope.ServiceProvider.GetRequiredService<IAdminService>();
            IGamesRepository repo = scope.ServiceProvider.GetRequiredService<IGamesRepository>();

            Sandbox sandbox = new(configuration, logger, adminService, service, repo);

            //sandbox.InitializeDatabase().Wait();
            sandbox.GetDatabaseData().Wait();
        }

        /// <summary>
        /// Builds/initiates .Common, .Business, and repository (.Database) services, as well as logger
        /// </summary>
        /// <param name="args">args from console</param>
        /// <returns>IHostBuilder to run .Build() on</returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            // prepare connection string
            string? connectionString = configuration.GetConnectionString($"DatabaseConnectionString");

            // create the host builder
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddCommonServices();
                    services.AddBusinessServices();
                    services.AddRepositoryServices(connectionString);

                    // add logger with singleton lifetime
                    services.AddScoped(x => logger);
                });
        }
    }
}