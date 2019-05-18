namespace FightSearch.Api
{
    using System;

    using FightSearch.Common;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    using NLog;
    using NLog.Web;

    public class Program
    {
        //private static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            Logger logger = NLogBuilder.ConfigureNLog("nlog.config")
                                       .GetCurrentClassLogger();
            logger.Info("app starting");
            try
            {
                logger.Info("Setting up host");
                IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args)
                                                 .UseStartup<Startup>()
                                                 .ConfigureAppConfiguration((hostingContext, config) =>
                                                 {
                                                     IHostingEnvironment environment = hostingContext.HostingEnvironment;
                                                     config.AddEnvironmentVariables();
                                                     ////.AddJsonFile("project.json", optional: true) // When app is published
                                                     ////.AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
                                                     //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                     //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                                                     //.AddJsonFile($"certificate.{env}.json", optional: true, reloadOnChange: true);

                                                     //IConfigurationRoot configurationRoot = config.Build();
                                                     ////IConfigurationSection certificateSettings = configurationRoot.GetSection("certificateSettings");
                                                     //Configuration = configurationRoot;
                                                     //SettingsProvider.SetConfiguration(configurationRoot);
                                                     //SettingsProvider.SetHostingEnvironment(environment);
                                                 })
                                                 .ConfigureLogging((hostContext, configLogging) =>
                                                 {
                                                     configLogging.ClearProviders();
                                                     configLogging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
                                                     //LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("DefaultConnection");
                                                 })
                                                 .UseNLog();
                logger.Info("Building host");
                IWebHost webHost = builder.Build();
                logger.Info("Run host");
                webHost.Run();
            }
            catch (Exception exception)
            {
                logger.Error($"Exception running app {exception}");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }

            //CreateWebHostBuilder(args).Build().Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //	WebHost.CreateDefaultBuilder(args)
        //		.UseStartup<Startup>();
    }
}