using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FightSearch.Repository.Sql;
using FightSearch.Repository.SqlLight;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using NLog.Web;

namespace PortSqlServerToSqlLite
{
    public class Program
    {
        private static IConfiguration Configuration { get; set; }
        private static ServiceProvider ServiceProvider { get; set; }
        public static async Task Main(string[] args)
        {
            Logger logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            Console.WriteLine("Hello World!");

            IHost host = new HostBuilder()
                .UseNLog()
                .ConfigureHostConfiguration(config =>
                {
                    //command line
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    string currentDirectory = Directory.GetCurrentDirectory();
                    string jsonFile = Path.Combine(currentDirectory, "appsettings.json");
                    if (!File.Exists(jsonFile))
                    {
                        currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "");
                    }

                    config.SetBasePath(currentDirectory);
                    config.AddJsonFile("appsettings.json", true);
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }

                    Configuration = config.Build();
                    Console.WriteLine(hostContext.HostingEnvironment.EnvironmentName);
                    string connectionString = Configuration.GetConnectionString("DefaultConnection");
                    //AppSettingOptions appSettings = Configuration.GetSection("AppSettingOptions").Get<AppSettingOptions>();
                           
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddEntityFrameworkSqlServer()
                        .AddDbContext<UfcContext>(options =>
                        {
                            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                            options.EnableSensitiveDataLogging(hostContext.HostingEnvironment.IsDevelopment());
                            options.EnableDetailedErrors(hostContext.HostingEnvironment.IsDevelopment());
                            //options.UseLoggerFactory(GetConsoleLoggerFactory());
                            //ServiceProvider x = services.BuildServiceProvider();
                            options.UseLoggerFactory(GetNLogLoggerFactory());
                                       
                        });
                    services.AddTransient<UfcContext>();
                    //services.AddScoped<IFightSearchEntities, UfcContext>();

                    // Database
                    DbContextOptionsBuilder<UfcContextLite> optionsBuilder = new DbContextOptionsBuilder<UfcContextLite>()
                        .UseSqlite(Configuration.GetConnectionString("SqLiteDefaultConnection"));
                    UfcContextLite context = new UfcContextLite(optionsBuilder.Options);
                    context.Database.EnsureCreated();
                    services.AddSingleton(optionsBuilder.Options);
                    
                    services.AddDbContextPool<UfcContextLite>(options =>
                        options.UseSqlite(Configuration.GetConnectionString("SqLiteDefaultConnection")));
                    services.AddSingleton<UfcContextLite>(context);


                    services.AddHostedService<PortSqlServerToSqlLiteHostedService>();

                    services.AddTransient<ISqLiteMigration,SqLiteMigration>();
                    ServiceProvider = services.BuildServiceProvider();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.ClearProviders();

                    // use nLog
                    //configLogging.AddNLog(new NLogProviderOptions {CaptureMessageTemplates = true, CaptureMessageProperties = true});
                    //configLogging.AddConsole();
                           
                    //configLogging.AddNLog(new NLogProviderOptions {CaptureMessageTemplates = true, CaptureMessageProperties = true});
                    //LogFactory factory = NLogBuilder.ConfigureNLog("nlog.config");
                    LogFactory test = NLogBuilder.ConfigureNLog("nlog.config");
                    //configLogging.AddNLog(new NLogProviderOptions {CaptureMessageTemplates = true, CaptureMessageProperties = true});
                    //NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = Configuration;
                    LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("DefaultConnection");

                    //var loggerFactory = ServiceProvider.GetService<ILoggerFactory>();
                    //loggerFactory.AddNLog();
                    //loggerFactory.ConfigureNLog("nlog.config");
                })
                .UseConsoleLifetime()
                .Build();
            using (host)
            {
                logger.Info("startinng .");
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                await host.StartAsync(token);
                await host.WaitForShutdownAsync(token);
                logger.Info("stoppig .");
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        private static ILoggerFactory GetNLogLoggerFactory()
        {

            LoggingConfiguration loggerConfig = new NLog.Config.LoggingConfiguration();
            FileTarget fileTarget = new NLog.Targets.FileTarget()
            {
                Name = "logfile",
                FileName = "log.txt",
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${event-context:item=EventId}|${message}|${ndc}"
            };
            loggerConfig.AddTarget(fileTarget);
            loggerConfig.LoggingRules.Add(new NLog.Config.LoggingRule("*", NLog.LogLevel.Info, fileTarget));
            //NLogLoggerFactory test = new NLogLoggerFactory(new NLogLoggerProvider(new NLogProviderOptions()));
            NLogLoggerFactory logLoggerFactory = new NLogLoggerFactory();
            NLogProviderOptions op = new NLogProviderOptions
            {
                CaptureMessageProperties = true, 
                CaptureMessageTemplates = true,
                EventIdSeparator = "|",
                IgnoreEmptyEventId = false,
                IncludeScopes = true,
                //ParseMessageTemplates = true
                //ShutdownOnDispose = true
            };

            NLogLoggerProvider p = new NLogLoggerProvider(op, loggerConfig.LogFactory);
            logLoggerFactory.AddProvider(p);
            return logLoggerFactory;
            //loggerFactory.AddNLog(new NLog.LogFactory(loggerConfig));
        }
    }
}
